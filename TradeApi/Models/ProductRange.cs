using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeApi.Models
{
    public class ProductRange
    {
        
        public int ProductRangeId { get; set; }

       
        [Required]
        public virtual Product Product { get; set; }
    }
}