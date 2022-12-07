using AutoMapper;
using ShoesShop.Domain;
using ShoesShop.DTO;
namespace ShoesShop.API.Mapper
{
    public class OrderMapper : Profile
    {
        public OrderMapper()
        {
            CreateMap<Order, OrderViewModel>()
               .ForMember(des => des.PaymentName, src => src.MapFrom(ent => (ent.Payment.PaymentName)));
        }
    }
}
