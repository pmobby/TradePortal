using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TradeApi.Models;
using TradeApi.ModelsDTO;
using AutoMapper;
using System.Data.SqlClient;
using System.Web.Http.Cors;

namespace TradeApi.Controllers
{
    [EnableCors(origins: "http://trade.littlebirdtoldme.co.uk,http://localhost:51518", headers: "*", methods: "*")]
    public class MessagesController : ApiController
    {
        private TradeApiContext db = new TradeApiContext();

        // GET: api/Messages/GetMessages/id/orderid
        public IQueryable<TPMessageDTO> GetMessages(int id, int itemid)
        {
            var customerid = new SqlParameter("@CustomerID", id);
            var porderid = new SqlParameter("@OrderID", itemid);

            var messages = db.Messages.SqlQuery("TPGetMessages @CustomerID, @OrderID", customerid, porderid);

            if (messages != null)
            {
                return messages.ToList().Select(Mapper.Map<TPMessage, TPMessageDTO>).AsQueryable();
            }
            else
            {
                return null;
            }

            
        }

        // GET: api/Messages/5
        [ResponseType(typeof(TPMessage))]
        public async Task<IHttpActionResult> GetMessage(int id)
        {
            TPMessage message = await db.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            return Ok(message);
        }

        // PUT: api/Messages/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMessage(int id, TPMessageDTO updmessage)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            var messageid = new SqlParameter("@MessageID", updmessage.MessageId);
            var orderid = new SqlParameter("@OrderID", updmessage.OrderId);
            var customerid = new SqlParameter("@CustomerID", updmessage.CustomerId);

            await db.Database.ExecuteSqlCommandAsync("TPPutMessageAcknowledge @MessageID, @OrderID, @CustomerID", messageid, orderid, customerid);
                        
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Messages
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostMessage(CreateMessageDTO message)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            var messageid = new SqlParameter("@MessageID", message.MessageId);
            var orderid = new SqlParameter("@OrderID", message.OrderId);
            var customerid = new SqlParameter("@CustomerID", message.CustomerId);
            var messagebody = new SqlParameter("@Message", message.Message);

            await db.Database.ExecuteSqlCommandAsync("TPPutMessageCreate @MessageID, @OrderID, @CustomerID, @Message", messageid, orderid, customerid, messagebody);

            return StatusCode(HttpStatusCode.NoContent);

            //return CreatedAtRoute("DefaultApi", new { id = message.MessageId }, message);
        }

        // DELETE: api/Messages/5
        [ResponseType(typeof(TPMessage))]
        public async Task<IHttpActionResult> DeleteMessage(int id)
        {
            TPMessage message = await db.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            db.Messages.Remove(message);
            await db.SaveChangesAsync();

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

        private bool MessageExists(int id)
        {
            return db.Messages.Count(e => e.MessageId == id) > 0;
        }
    }
}