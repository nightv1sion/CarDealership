using AutoMapper;
using WebAPI.Models;
using WebAPI.Shared.DataTransferObjects;

namespace WebAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DealerShop, DealerShopCreationDTO>().ReverseMap()
                .ForMember(m => m.Location, opt => opt.Ignore());
            CreateMap<DealerShop, DealerShopDTO>().ReverseMap();
        }
    }
}
