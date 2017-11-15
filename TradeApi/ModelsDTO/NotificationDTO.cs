using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TradeApi.ModelsDTO
{
    public class NotificationDTO
    {
        [Key]
        public int NotificationId { get; set; }
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public string Notification { get; set; }
        public DateTime NotificationDateTime { get; set; }
        public bool NotificationRead { get; set; }
    }
}