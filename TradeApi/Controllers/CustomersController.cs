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

namespace TradeApi.Controllers
{
    [EnableCors(origins: "http://trade.littlebirdtoldme.co.uk,http://localhost:51518", headers: "*", methods: "*")]
    public class CustomersController : ApiController
    {
        private TradeApiContext db = new TradeApiContext();

        // GET: api/Customers
        public IQueryable<CustomerDTO> GetCustomers()
        {
            return db.Customers.ToList().Select(Mapper.Map<Customer, CustomerDTO>).AsQueryable();
        }

        // GET: api/Customers/5
        [ResponseType(typeof(CustomerDTO))]
        public async Task<IHttpActionResult> GetCustomer(int id)
        {            
            var custIdParam = new SqlParameter("@CustomerID", id);

            var customers = await db.Customers.SqlQuery("TPGetCustomer @CustomerID", custIdParam).SingleOrDefaultAsync();

            if (customers != null)
            {
                return Ok(Mapper.Map<Customer, CustomerDTO>(customers));
            }
            else
            {
                return NotFound();
            }        

            
        }

        // PUT: api/Customers/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            db.Entry(customer).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/Customers
        [HttpPost]
        [ResponseType(typeof(CustomerDTO))]
        public async Task<IHttpActionResult> PostCustomer(CustomerDTO customerdto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var customer = Mapper.Map<CustomerDTO, Customer>(customerdto);
            db.Customers.Add(customer);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = customer.CustomerId }, customerdto);
        }

        // DELETE: api/Customers/5
        [ResponseType(typeof(Customer))]
        public async Task<IHttpActionResult> DeleteCustomer(int id)
        {
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            db.Customers.Remove(customer);
            await db.SaveChangesAsync();

            return Ok(customer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(int id)
        {
            return db.Customers.Count(e => e.CustomerId == id) > 0;
        }
    }
}