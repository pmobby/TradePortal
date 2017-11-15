using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeApi.Models
{
    public class MessageNotification
    {
        [Key]
        public int NotificationId { get; set; }
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public string Notification { get; set; }
        public DateTime NotificationDateTime { get; set; }
        public bool NotificationRead { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual ICollection<TPMessage> TPMessages { get; set; }
    }
}