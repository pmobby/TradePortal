using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeApi.Models
{
    public class Order
    {
        public int CustomerId { get; set; }        
        [Key]
        public int OrderId { get; set; }      
        public string OrderStatus { get; set; }
        public DateTime? EstimatedDispatchDate { get; set; }
        public decimal OrderValue { get; set; }
        public decimal DeliveryValue { get; set; }
        public decimal TotalOrderValueIncVAT { get; set; }
        public decimal TotalOrderValueExcVAT { get; set; }
        public int ProductCount { get; set; }

        public virtual Customer Customer { get; set; } 

        public ICollection<TPMessage> Messages { get; set; }        
                
        public ICollection<OrderItem> OrderItems { get; set; }
        //product count


    }
}