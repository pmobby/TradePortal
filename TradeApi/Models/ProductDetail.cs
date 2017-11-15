using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TradeApi.ModelsDTO;

namespace TradeApi.Models
{
    public class ProductDetail
    {
        [Key]
        public int ProductId { get; set; }
        public int WebProductID { get; set; }
        public string AgeNote { get; set; }
        public string SecondNote { get; set; }
        public string BuyLine { get; set; }
        public string FeaturesList { get; set; }
        public string SpecificationList { get; set; }
        public string ImagePrimary { get; set; }
        public string ImagePrimaryText { get; set; }
        public string ImageSecondary1 { get; set; }
        public string ImageSecondary1Text { get; set; }
        public string ImageSecondary2 { get; set; }
        public string ImageSecondary2Text { get; set; }
        public string ImageSecondary3 { get; set; }
        public string ImageSecondary3Text { get; set; }
        public string ImageSecondary4 { get; set; }
        public string ImageSecondary4Text { get; set; }
        public string ImageSecondary5 { get; set; }
        public string ImageSecondary5Text { get; set; }
        public string ImageSizeURL { get; set; }
        public string ImageSizeText { get; set; }
        public string ImageSizeGuideURL { get; set; }
        public string ImageSizeGuideText { get; set; }

        public Product Product { get; set; }
    }
}