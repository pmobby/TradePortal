using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TradeApi.ModelsDTO
{
    public class TPMessageDTO
    {
        public int MessageId { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string Message { get; set; }
        public DateTime SentDateTime { get; set; }
        public bool Acknowledged { get; set; }
    }
}