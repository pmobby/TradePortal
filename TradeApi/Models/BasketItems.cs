using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TradeApi.Models
{
    public class BasketItems
    {
        public int ProductId { get; set; }
        public int VariantId { get; set; }
        public int PriceTypeId { get; set; }
        public double ItemCost { get; set; }
        public int ItemQuantity { get; set; }
        public double NetCost { get; set; }
    }
}