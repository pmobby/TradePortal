using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TradeApi.Models;
using TradeApi.ModelsDTO;

namespace TradeApi.Models
{
    public class TPMessage
    {
        [Key]
        public int MessageId { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string Message { get; set; }
        public DateTime SentDateTime { get; set; }
        public bool Acknowledged { get; set; }

        //[Column(Order = 1)]
        public Order Order { get; set; }
        

        //[Column(Order = 2)]
        public MessageNotification Notification { get; set; }
    }
}