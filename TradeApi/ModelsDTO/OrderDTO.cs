using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TradeApi.Models;

namespace TradeApi.ModelsDTO
{
    public class OrderDTO
    {
        public int CustomerId { get; set; }
        [Key]
        public int OrderId { get; set; }
        public string OrderStatus { get; set; }
        public DateTime? EstimatedDispatchDate { get; set; }
        public double OrderValue { get; set; }
        public double DeliveryValue { get; set; }
        public double TotalOrderValueIncVAT { get; set; }
        public double TotalOrderValueExcVAT { get; set; }
        public int ProductCount { get; set; }
        //public List<OrderItem> OrderItem{ get; set; }
    }
}