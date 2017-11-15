using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TradeApi.Models
{
    public class Product
    {
        public int CustomerId { get; set; }
        public string ProductRange { get; set; }
        public string ProductRangeType { get; set; }
        public int ProductId { get; set; }
        public int? ProductVariantID { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal RRP { get; set; }
        public int OfferPriceID { get; set; }
        public double OfferPrice { get; set; }
        public int StandardPriceID { get; set; }
        public decimal StandardPrice { get; set; }
        public int? PackQuantityOuter { get; set; }
        public int? PackQuantityInner { get; set; }
        public string ProductCodeAdditional { get; set; }
        public bool TagBestSeller { get; set; }
        public bool TagNew { get; set; }
        public bool TagToBeDiscontinued { get; set; }
        public string OutOfStockMessage { get; set; }
        public bool MaxOrder { get; set; }
        public int MaxOrderQuantity { get; set; }
        public string StockStatus { get; set; }
        public int? RangeSortOrder { get; set; }
        public int? RangeID { get; set; }

        public ICollection<ProductDetail> ProductDetails { get; set; }

        //public ProductDetail ProductDetails { get; set; }
        //public string Description { get; set; }
        //public string ImageFileName { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public virtual CustomerPrice CustomerPrice { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ICollection<ProductRange> ProductRanges { get; set; }
        public virtual ICollection<ProductOffer> ProductOffers { get; set; }
    }
}