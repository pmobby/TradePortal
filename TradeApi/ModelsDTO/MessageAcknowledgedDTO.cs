using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TradeApi.Models;

namespace TradeApi.ModelsDTO
{
    public class MessageAcknowledgedDTO
    {
        public int MessageId { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public TPMessage Message { get; set; }
    }
}