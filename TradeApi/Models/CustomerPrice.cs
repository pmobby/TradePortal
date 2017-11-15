using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeApi.Models
{
    public class CustomerPrice
    {
        [ForeignKey("Product")]
        public int CustomerPriceId { get; set; }
        
        
        public decimal RRP { get; set; }
        public decimal List { get; set; }
        public decimal OfferPrice { get; set; }
        public decimal SpecialOffer { get; set; }

        
        public virtual Product Product { get; set; }
    }
}