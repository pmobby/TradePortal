using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TradeApi.Models
{
    public class LittleBirdWebsiteContext : DbContext
    {
        public LittleBirdWebsiteContext() : base("name=LittleBirdWebsite")
        {
            Database.SetInitializer<LittleBirdWebsiteContext>(null);
        }
                

        public System.Data.Entity.DbSet<TradeApi.Models.ProductDetail> ProductDetails { get; set; }
        
    }
}