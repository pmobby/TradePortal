using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TradeApi.Models;
using TradeApi.ModelsDTO;
using AutoMapper;
using System.Web.Http.Cors;
using TradeApi.CustomResponse;

namespace TradeApi.Controllers
{    
    [EnableCors(origins: "http://trade.littlebirdtoldme.co.uk,http://localhost:51518", headers: "*", methods: "*")]    
    public class OrdersController : ApiController
    {
        private TradeApiContext db = new TradeApiContext();

        // GET: api/Orders/GetOrders/5
        public IQueryable<OrderDTO> GetOrders(int id)
        {            
            var custIdParam = new SqlParameter("@CustomerID", id);
            var orders = db.Orders.SqlQuery("TPGetOrders @CustomerID", custIdParam);

            if (orders != null)
            {
                return orders.ToList().Select(Mapper.Map<Order, OrderDTO>).AsQueryable();
            }
            else
            {
                return null;
            }          
            
        }
        // GET: api/Orders/GetOrder/5/6 - Get Order Level Value
        [ResponseType(typeof(OrderDtoWithItem))]
        public async Task<IHttpActionResult> GetOrder(int id, int itemid)
        {
            var custidparam = new SqlParameter("@CustomerID", id);
            var orderidparam = new SqlParameter("@OrderID", itemid);

            var cid = new SqlParameter("@CustomerID", id);
            var orderidp = new SqlParameter("@OrderID", itemid);

            var order = await db.Orders.SqlQuery("TPGetOrder @CustomerID, @OrderID", custidparam, orderidparam).SingleAsync();
            var orderItems = await db.Database.SqlQuery<OrderItem>("TPGetOrderLines @CustomerID, @OrderID", cid, orderidp).ToListAsync();                    
                        
            order.OrderItems = orderItems;
            
            if(order != null)
            {
                return Ok(Mapper.Map<Order, OrderDtoWithItem>(order));
            }
            else
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No order with ID = {0}", itemid)),
                    ReasonPhrase = "Order ID Not Found"
                };
                throw new HttpResponseException(resp);
                //return NotFound();
            } 

        }

        // GET: api/Orders/GetOrderLines/id/orderid
        //[HttpGet]
        //[ResponseType(typeof(OrderItemDTO))]
        //public async Task<IHttpActionResult> GetOrderLines(int id, int itemid)
        //{
        //    var cidparam = new SqlParameter("@CustomerID", id);
        //    var odidparam = new SqlParameter("@OrderID", itemid);

        //    var orderLine = await db.Database.SqlQuery<OrderItem>("TPGetOrderLines @CustomerID, @OrderID", cidparam, odidparam).ToListAsync();

        //    if (orderLine.Count() > 0)
        //    {
        //        return Ok(orderLine.ToList().Select(Mapper.Map<OrderItem, OrderItemDTO>));
        //    }
        //    else
        //    {
        //        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
        //        {
        //            Content = new StringContent(string.Format("No order items")),
        //            ReasonPhrase = "Order Items Not Found"
        //        };
        //        throw new HttpResponseException(resp);
        //    }
        //}

        [HttpGet]
        [ResponseType(typeof(NewOrderIdDto))]
        public async Task<IHttpActionResult> GetOrderId(int id)
        {
            var oiddto = new NewOrderIdDto();
            var cid = new SqlParameter("@CustomerID", id);
            var outParam = new SqlParameter();
            outParam.ParameterName = "@OrderID";
            outParam.SqlDbType = SqlDbType.Int;
            outParam.Direction = ParameterDirection.Output;
            //var orderid = await db.Database.SqlQuery<int>("TPPutOrderCreate @OrderID OUT, @CustomerID", outParam, cid).FirstOrDefaultAsync();
            var orderid = await db.Database.ExecuteSqlCommandAsync("TPPutOrderCreate @OrderID OUT, @CustomerID", outParam, cid);

            var od = (int)outParam.Value;
            oiddto.OrderId = od;
            return Ok(oiddto);
        }       


        // PUT: api/Orders/PutOrder/5        
        [AcceptVerbs("POST")]        
        [ResponseType(typeof(NewOrderIdDto))]
        public async Task<IHttpActionResult> PostCreateOrder([FromUri]int id, [FromBody]PutOrderDTO podto)
        {
            var oiddto = new NewOrderIdDto();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (podto != null)
            {       
                //call to create order record and receive order id
                var cid = new SqlParameter("@CustomerID", id);
                var outParam = new SqlParameter();
                outParam.ParameterName = "@OrderID";
                outParam.SqlDbType = SqlDbType.Int;
                outParam.Direction = ParameterDirection.Output;
                var orderid = await db.Database.ExecuteSqlCommandAsync("TPPutOrderCreate @OrderID OUT, @CustomerID", outParam, cid);

                var od = (int)outParam.Value;

                //update order table
                var status = 1;
                var EstimatedDispatchDate = DateTime.Now;
                var custid = new SqlParameter("@CustomerID", id);
                var orderidparam = new SqlParameter("@OrderID", od);
                var ordstatusparam = new SqlParameter("@OrderStatus", status);
                var eddparam = new SqlParameter("@EstimatedDispatchDate", EstimatedDispatchDate);
                var ordervalueparam = new SqlParameter("@OrderValue", podto.OrderValue);
                var delcostparam = new SqlParameter("@DeliveryCost", podto.DeliveryValue);

                await db.Database.ExecuteSqlCommandAsync("TPPutOrder @CustomerID, @OrderID, @OrderStatus, @EstimatedDispatchDate, @OrderValue, @DeliveryCost", custid, orderidparam, ordstatusparam, eddparam, ordervalueparam, delcostparam);

                //call to get orderline id
                var orderidcreated = new SqlParameter("@OrderID", od);
                var outp = new SqlParameter();
                outp.ParameterName = "@OrderLineID";
                outp.SqlDbType = SqlDbType.Int;
                outp.Direction = ParameterDirection.Output;
                var orderlineid = await db.Database.ExecuteSqlCommandAsync("TPPutOrderLineCreate @OrderLineID OUT, @OrderID", outp, orderidcreated);

                var outputoderlineid = (int)outp.Value;

                //update order line table for each item in the basket
                foreach (var item in podto.BasketItems)
                {
                    var olineid = new SqlParameter("@OrderID", od);
                    var productid = new SqlParameter("@ProductID", item.ProductId);
                    var variantid = new SqlParameter("@VariantID", item.VariantId);
                    var pricetypeid = new SqlParameter("@PriceTypeID", item.PriceTypeId);
                    var itemcost = new SqlParameter("@ItemCost", item.ItemCost);
                    var itemquantity = new SqlParameter("@ItemQuantity", item.ItemQuantity);
                    var netcost = new SqlParameter("@NetCost", item.NetCost);

                    await db.Database.ExecuteSqlCommandAsync("TPPutOrderLine @OrderID, @ProductID, @VariantID, @PriceTypeID, @ItemCost, @ItemQuantity, @NetCost", olineid, productid, variantid, pricetypeid, itemcost, itemquantity, netcost);
                }

                oiddto.OrderId = od;
                return Ok(oiddto);
                    //return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                oiddto.CustomerId = id;
                oiddto.Message = "Basket Item is empty! Cannot create order";
                return Ok(oiddto);
            }
        }

        // POST: api/Orders
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostOrders([FromBody]PostOrderDTO submitorder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                try
                {
                    var cid = new SqlParameter("@CustomerID", submitorder.CustomerId);
                    var oid = new SqlParameter("@OrderID", submitorder.OrderId);
                    var pon = new SqlParameter("@PONumber", submitorder.PONumber);
                    var edd = new SqlParameter("@EstimatedDispatchDate", submitorder.EstimatedDispatchDate);
                    var on = new SqlParameter("@OrderNotes", submitorder.OrderNotes);
                    var bin = new SqlParameter("@BookingInNotes", submitorder.BookingInNotes);
                    var dsname = new SqlParameter("@DropShipCustomerName", submitorder.DropShipCustomerName);
                    var dsad = new SqlParameter("@DropShipAddress1", submitorder.DropShipAddress1);
                    var dsadd = new SqlParameter("@DropShipAddress2", submitorder.DropShipAddress2);
                    var dsaddd = new SqlParameter("@DropShipAddress3", submitorder.DropShipAddress3);
                    var dspo = new SqlParameter("@DropShipPostCode", submitorder.DropShipPostCode);
                    var go = new SqlParameter("@GiftOrderYesNo", submitorder.GiftOrderYesNo);
                    var goname = new SqlParameter("@GiftOrderCustomerName", submitorder.GiftOrderCustomerName);

                    await db.Database.ExecuteSqlCommandAsync("TPPutOrderSubmit @CustomerID, @OrderID, @PONumber, @EstimatedDispatchDate, @OrderNotes, @BookingInNotes, @DropShipCustomerName, @DropShipAddress1, @DropShipAddress2, @DropShipAddress3, @DropShipPostCode, @GiftOrderYesNo, @GiftOrderCustomerName", cid, oid, pon, edd, on, bin, dsname, dsad, dsadd, dsaddd, dspo, go, goname);

                    //return StatusCode(HttpStatusCode.NoContent);
                }
                catch 
                {
                    throw new HttpResponseException(HttpStatusCode.InternalServerError);

                }
            }
            return new TextResult("Order Submitted Successfully", Request);

            //return CreatedAtRoute("DefaultApi", new { id = order.OrderId }, order);
        }

        // DELETE: api/Orders/5        
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> GetOrderRemoved(int id)
        {
            
            var orderid = new SqlParameter("@OrderID", id);
            await db.Database.ExecuteSqlCommandAsync("TPPutOrderDiscard @OrderID", orderid);

            //db.Orders.Remove(order);
            //await db.SaveChangesAsync();
            string message = "Order #" + id + " is discarded";
            return Ok(message);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(int id)
        {
            return db.Orders.Count(e => e.OrderId == id) > 0;
        }
    }
}