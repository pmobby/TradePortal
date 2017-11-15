using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeApi.Models
{
    public class OrderItem
    {
        public int CustomerId { get; set; }
        [Key]
        public int OrderId { get; set; }        
        public string ProductName { get; set; }
        public int ItemQuantity { get; set; }
        public double ItemCost { get; set; }
        public double NetCost { get; set; }
        public double DiscountValue { get; set; }
        public double TotalValue { get; set; }
        public double VAT { get; set; }
        public string PriceTypeName { get; set; }
        public string PriceTypeDescription { get; set; }  

        //[Column(Order = 1)]
        public Order Order { get; set; }

        //[Column(Order = 2)]
        public Product Product { get; set; }
    }
}