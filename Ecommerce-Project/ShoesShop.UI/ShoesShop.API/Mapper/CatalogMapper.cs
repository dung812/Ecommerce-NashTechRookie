using AutoMapper;
using ShoesShop.Domain;
using ShoesShop.DTO;

namespace ShoesShop.API.Mapper
{
    public class CatalogMapper : Profile
    {
        public CatalogMapper()
        {
            CreateMap<Catalog, CatalogCreateModel>();
        }
    }
}
