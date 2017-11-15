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
using System.Web.Http.Cors;
using System.Web.Http.Description;
using TradeApi.Models;
using AutoMapper;
using TradeApi.ModelsDTO;

namespace TradeApi.Controllers
{
    [EnableCors(origins: "http://trade.littlebirdtoldme.co.uk,http://localhost:51518", headers: "*", methods: "*")]
    public class NotificationsController : ApiController
    {
        private TradeApiContext db = new TradeApiContext();

        // GET: api/Notifications/GetNotifications/id
        public IQueryable<NotificationDTO> GetNotifications(int id)
        {
            var customerid = new SqlParameter("@CustomerID", id);
            var notifications = db.Notifications.SqlQuery("TPGetNotifications @CustomerID", customerid);
            if(notifications != null)
            {
                return notifications.ToList().Select(Mapper.Map<MessageNotification, NotificationDTO>).AsQueryable();
            }
            else
            {
                return null;
            }
        }

        // GET: api/Notifications/GetNotification/id
        [ResponseType(typeof(MessageNotification))]
        public async Task<IHttpActionResult> GetNotification(int id)
        {
            MessageNotification notification = await db.Notifications.FindAsync(id);
            if (notification == null)
            {
                return NotFound();
            }

            return Ok(notification);
        }

        // PUT: api/Notifications/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutNotification(int id)
        {
            var customerid = new SqlParameter("@CustomerID", id);

            await db.Database.ExecuteSqlCommandAsync("TPPutNotifications", customerid);
           
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Notifications
        [ResponseType(typeof(MessageNotification))]
        public async Task<IHttpActionResult> PostNotification(MessageNotification notification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Notifications.Add(notification);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = notification.NotificationId }, notification);
        }

        // DELETE: api/Notifications/5
        [ResponseType(typeof(MessageNotification))]
        public async Task<IHttpActionResult> DeleteNotification(int id)
        {
            MessageNotification notification = await db.Notifications.FindAsync(id);
            if (notification == null)
            {
                return NotFound();
            }

            db.Notifications.Remove(notification);
            await db.SaveChangesAsync();

            return Ok(notification);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NotificationExists(int id)
        {
            return db.Notifications.Count(e => e.NotificationId == id) > 0;
        }
    }
}