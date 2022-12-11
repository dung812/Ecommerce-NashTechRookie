using AutoMapper;
using ShoesShop.Domain;
using ShoesShop.DTO.Admin;

namespace ShoesShop.API.Mapper
{
    public class AdminMapper : Profile
    {
        public AdminMapper()
        {
            CreateMap<Admin, AdminViewModel>()
               .ForMember(des => des.Gender, src => src.MapFrom(ent => (ent.Gender == Domain.Enum.Gender.Men ? "Men" : "Women")))
               .ForMember(des => des.RoleName, src => src.MapFrom(ent => (ent.Role.RoleName)));
        }
    }
}
