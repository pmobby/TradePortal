using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TradeApi.ModelsDTO
{
    public class NewOrderIdDto
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string Message { get; set; }
    }
}