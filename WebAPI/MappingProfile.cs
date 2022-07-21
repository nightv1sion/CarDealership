using AutoMapper;
using WebAPI.Models;
using WebAPI.Shared.DataTransferObjects;

namespace WebAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DealerShop, DealerShopCreationDTO>().ReverseMap();
            CreateMap<DealerShop, DealerShopDTO>().ReverseMap();
        }
    }
}
