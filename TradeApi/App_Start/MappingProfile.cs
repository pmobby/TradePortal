using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TradeApi.Models;
using TradeApi.ModelsDTO;

namespace TradeApi.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDTO>();
            CreateMap<CustomerPrice, CustomerPriceDTO>();
            CreateMap<Order, OrderDTO>();
            CreateMap<Order, OrderDtoWithItem>();
            CreateMap<OrderItem, OrderItemDTO>();
            CreateMap<Product, ProductDTO>();
            CreateMap<Product, ProductDTOWithDetails>();
            CreateMap<GetProduct, GetProductDTO>();
            CreateMap<ProductDetail, ProductDetailDTO>();
            CreateMap<ProductCategory, ProductCategoryDTO>();
            CreateMap<ProductOffer, ProductOffer>();
            CreateMap<ProductRange, ProductRangeDTO>();            
            CreateMap<MessageNotification, NotificationDTO>();
            CreateMap<TPMessage, TPMessageDTO>();
            CreateMap<TPMessageDTO, TPMessage>();
        }
    }
}