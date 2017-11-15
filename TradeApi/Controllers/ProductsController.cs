using AutoMapper;
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
using System.Web.Http.Cors;
using System.Globalization;

namespace TradeApi.Controllers
{
    [EnableCors(origins: "http://trade.littlebirdtoldme.co.uk,http://localhost:51518", headers: "*", methods: "*")]
    public class ProductsController : ApiController
    {
        private TradeApiContext db = new TradeApiContext();
        private LittleBirdWebsiteContext lb = new LittleBirdWebsiteContext();        

        // GET: api/Products/GetProducts/5
        public IQueryable<ProductDTOWithDetails> GetProducts(int id)
        {
            var customerId = new SqlParameter("@CustomerID", id);
            var products = db.Database.SqlQuery<Product>("TPGetProducts @CustomerID", customerId).ToList();            
            //var productdetails = lb.ProductDetails.SqlQuery("TPGetProductsDetails");
            foreach (var proditem in products)
            {
                var prodid = proditem.ProductId;
                var prodidparam = new SqlParameter("@ProductID", prodid);
                var productdetail = lb.Database.SqlQuery<ProductDetail>("TPGetProductDetails @ProductID", prodidparam).ToList();
                //var productdetail = lb.ProductDetails.SqlQuery("TPGetProductDetails @ProductID", prodidparam).ToList();
                proditem.ProductDetails = productdetail;
                
            }

            if (products != null)
            {
                return products.ToList().Select(Mapper.Map<Product, ProductDTOWithDetails>).AsQueryable();
            }
            else
            {
                return null;
            }
            
        }

        // GET: api/Products/SearchProduct/id
        [HttpGet]
        public IQueryable<ProductDTOWithDetails> SearchProduct(int id, string term)
        {
            CultureInfo ci = new CultureInfo("en-GB");
            var customerId = new SqlParameter("@CustomerID", id);
            var products = db.Database.SqlQuery<Product>("TPGetProducts @CustomerID", customerId).ToList();
            var productsByNameFilter = products.Where(s => s.ProductName.StartsWith(term, true, ci)).ToList();
            

            foreach (var proditem in productsByNameFilter)
            {
                var prodid = proditem.ProductId;
                var prodidparam = new SqlParameter("@ProductID", prodid);
                var productdetail = lb.Database.SqlQuery<ProductDetail>("TPGetProductDetails @ProductID", prodidparam).ToList();
                //var productdetail = lb.ProductDetails.SqlQuery("TPGetProductDetails @ProductID", prodidparam).ToList();
                proditem.ProductDetails = productdetail;

            }

            if (productsByNameFilter != null)
            {
                return productsByNameFilter.ToList().Select(Mapper.Map<Product, ProductDTOWithDetails>).AsQueryable();
            }
            else
            {
                return null;
            }
        }

        // GET: api/Products/GetProduct/id/productid
        [HttpGet]
        [ResponseType(typeof(GetProductDTO))]
        public async Task<IHttpActionResult> GetProduct(int id, int itemid)
        {   
            // productid
            var customerId = new SqlParameter("@CustomerID", id);
            var prodId = new SqlParameter("@ProductID", itemid);
            var productId = new SqlParameter("@ProductID", itemid);

            // TPGetProduct sp returns result without 'RangeSortOrder' and 'RangeID' columns.
            // Therefore, a different class entity 'GetProducts' is used to represent TPGetProduct result, otherwise, it breaks

            var products = await db.Database.SqlQuery<GetProduct>("TPGetProduct @CustomerID, @ProductID", customerId, prodId).ToListAsync();

            var productdetails = await lb.ProductDetails.SqlQuery("TPGetProductDetails @ProductID", productId).FirstOrDefaultAsync();

            foreach(var prodinstance in products)
            {
                prodinstance.ProductDetail = productdetails;
            }                      

            if (products != null)
            {
                return Ok(products.ToList().Select(Mapper.Map<GetProduct, GetProductDTO>));
            }
            else
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No product with ID = {0}", itemid)),
                    ReasonPhrase = "Product Not Found"
                };
                throw new HttpResponseException(resp);
            }
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.ProductId)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Products
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(product);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> DeleteProduct(int id)
        {
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            await db.SaveChangesAsync();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
                lb.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.ProductId == id) > 0;
        }
    }
}