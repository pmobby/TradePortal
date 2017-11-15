using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TradeApi.ModelsDTO
{
    public class CreateMessageDTO
    {
        [Key]
        public int MessageId { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string Message { get; set; }
    }
}