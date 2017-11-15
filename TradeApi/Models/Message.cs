using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeApi.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        
        public string MessageBody { get; set; }
        public string SenderType { get; set; }
        public string Status { get; set; }

        
        [Column(Order = 1)]
        public virtual Order Order { get; set; }

        
        [Column(Order = 2)]
        public virtual Notification Notification { get; set; }
    }
}