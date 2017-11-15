using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeApi.Models
{
    public class ProductCategory
    {
        [ForeignKey("Product")]
        public int ProductCategoryId { get; set; }
        
        

        
        public virtual Product Product { get; set; }
    }
}