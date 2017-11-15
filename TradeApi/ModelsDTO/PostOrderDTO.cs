using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TradeApi.ModelsDTO
{
    public class PostOrderDTO
    {
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public string PONumber { get; set; }
        public DateTime EstimatedDispatchDate { get; set; }
        public string OrderNotes { get; set; }
        public string BookingInNotes { get; set; }
        public string DropShipCustomerName { get; set; }
        public string DropShipAddress1 { get; set; }
        public string DropShipAddress2 { get; set; }
        public string DropShipAddress3 { get; set; }
        public string DropShipPostCode { get; set; }
        public bool GiftOrderYesNo { get; set; }
        public string GiftOrderCustomerName { get; set; }
    }
}