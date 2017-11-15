using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeApi.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        
        
        public string Status { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }

        
        //public virtual Customer Customer { get; set; }
        public virtual ICollection<TPMessage> UnreadMessages { get; set; }
    }
}