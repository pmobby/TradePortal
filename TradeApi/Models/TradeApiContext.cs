using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TradeApi.Models
{
    public class TradeApiContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public TradeApiContext() : base("name=TradeApiContext")
        {
        }

        public System.Data.Entity.DbSet<TradeApi.Models.Customer> Customers { get; set; }

        public System.Data.Entity.DbSet<TradeApi.Models.Address> Addresses { get; set; }

        public System.Data.Entity.DbSet<TradeApi.Models.Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public System.Data.Entity.DbSet<TradeApi.Models.Product> Products { get; set; }

        public System.Data.Entity.DbSet<TradeApi.Models.CustomerPrice> CustomerPrices { get; set; }

        public System.Data.Entity.DbSet<TradeApi.Models.ProductCategory> ProductCategories { get; set; }

        public System.Data.Entity.DbSet<TradeApi.Models.TPMessage> Messages { get; set; }

        public System.Data.Entity.DbSet<TradeApi.Models.MessageNotification> Notifications { get; set; }

        public DbSet<GetProduct> GetProducts { get; set; }
    }
}
