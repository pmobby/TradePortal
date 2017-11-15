using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TradeApi.Models;

namespace TradeApi.ModelsDTO
{
    public class PutOrderDTO
    {
        public double OrderValue { get; set; }
        public double DeliveryValue { get; set; }
        public List<BasketItems> BasketItems { get; set; }

        //public DateTime EstimatedDispatchDate { get; set; }        

    }
}