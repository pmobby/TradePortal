using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using TradeApi.Models;

namespace TradeApi.ModelsDTO
{
    public class CustomerDTO
    {
        [Required]
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string TelephoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string ContactNumber { get; set; }
        public string EmailAddress { get; set; }
        public string DeliveryAddressBuildingName { get; set; }
        public string DeliveryAddressBuildingNumber { get; set; }
        public string DeliveryAddressStreetName { get; set; }
        public string DeliveryAddressLocality { get; set; }
        public string DeliveryAddressTown { get; set; }
        public string DeliveryAddressCounty { get; set; }
        public string DeliveryAddressPostCode { get; set; }
        public string DeliveryAddressCountry { get; set; }
        public string DeliveryAddressTelephoneNumber { get; set; }
        public string DeliveryAddressEmailAddress { get; set; }
        public string ShopAddressBuildingName { get; set; }
        public string ShopAddressBuildingNumber { get; set; }
        public string ShopAddressStreetName { get; set; }
        public string ShopAddressLocality { get; set; }
        public string ShopAddressTown { get; set; }
        public string ShopAddressCounty { get; set; }
        public string ShopAddressPostCode { get; set; }
        public string ShopAddressCountry { get; set; }
        public string ShopAddressTelephoneNumber { get; set; }
        public string ShopAddressEmailAddress { get; set; }
        public string BillingAddressBuildingName { get; set; }
        public string BillingAddressBuildingNumber { get; set; }
        public string BillingAddressStreetName { get; set; }
        public string BillingAddressLocality { get; set; }
        public string BillingAddressTown { get; set; }
        public string BillingAddressCounty { get; set; }
        public string BillingAddressPostCode { get; set; }
        public string BillingAddressCountry { get; set; }
        public string BillingAddressContactName { get; set; }
        public string BillingAddressTelephoneNumber { get; set; }
        public string BillingAddressEmailAddress { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingAddressBuildingNumber { get; set; }
        public string ShippingAddressStreetName { get; set; }
        public string ShippingAddressLocality { get; set; }
        public string ShippingAddressTown { get; set; }
        public string ShippingAddressCounty { get; set; }
        public string ShippingAddressPostCode { get; set; }
        public string ShippingAddressCountry { get; set; }

        public bool BookingRequired { get; set; }

        public string BookingContact { get; set; }
        public string BookingTelephoneNumber { get; set; }
        public string BookingEmailAddress { get; set; }

        public int BookingLeadTimeDays { get; set; }

        public string OrdersContactNamePrimary { get; set; }
        public string OrdersContactNameSecondary { get; set; }

        public bool PickAndMix { get; set; }
        public bool DropShipping { get; set; }
        public string DropShippingContactNumber { get; set; }

        public int OfferPriceID { get; set; }
        public string OfferPriceName { get; set; }
        public string OfferPriceDescription { get; set; }
        public string OfferPriceCurrencySymbol { get; set; }
        public string OfferPriceCurrencyName { get; set; }

        public string OfferPriceCurrencyCode { get; set; }

        public string OfferPackSizeType { get; set; }
        public int StandardPriceID { get; set; }
        public string StandardPriceName { get; set; }
        public string StandardPriceDescription { get; set; }
        public string StandardPackSizeType { get; set; }
        public string StandardPriceCurrencySymbol { get; set; }
        public string StandardPriceCurrencyName { get; set; }
        public string StandardPriceCurrencyCode { get; set; }
        public bool ShowRRP { get; set; }
        public bool ShowStandard { get; set; }
        public double QualificationValue { get; set; }
        public double DeliveryStandardPrice { get; set; }
        public double DeliveryQualifiedPrice { get; set; }
        public int NotificationsUnreadCount { get; set; }

        //public virtual ICollection<Order> Orders { get; set; }
    }
}