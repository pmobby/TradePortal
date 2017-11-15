using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TradeApi.Models;
using System.ComponentModel.DataAnnotations;

namespace TradeApi.ModelsDTO
{
    public class OrderDtoWithItem
    {
        public int CustomerId { get; set; }
        [Key]
        public int OrderId { get; set; }
        public string OrderStatus { get; set; }
        public DateTime EstimatedDispatchDate { get; set; }
        public double OrderValue { get; set; }
        public double DeliveryValue { get; set; }
        public double TotalOrderValueIncVAT { get; set; }
        public double TotalOrderValueExcVAT { get; set; }
        public int ProductCount { get; set; }
        public ICollection<OrderItemDTO> OrderItems{ get; set; }
    }
}