using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeApi.Models
{
    public class ProductOffer
    {
        
        public int ProductOfferId { get; set; }        
        
        
        public string OfferType { get; set; }
        public string Description { get; set; }

        [Required]
        public virtual Product Product { get; set; }
    }
}