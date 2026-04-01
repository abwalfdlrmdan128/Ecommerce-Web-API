using AutoMapper;
using Ecommerce.Core.DTOS;
using Ecommerce.Core.Models;
using Ecommerce.Core.Models.Order;

namespace EcommerceAPI.Mapping
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<Orders, OrderToReturnDTO>()
                .ForMember(d => d.deliveryMethod,
                o => o.
                MapFrom(s => s.deliveryMethod.Name))
                .ReverseMap();

            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
            CreateMap<ShippingAddress, ShipAddressDTO>().ReverseMap();
            CreateMap<Address, ShipAddressDTO>().ReverseMap();
        }
    }
}
