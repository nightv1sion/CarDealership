using AutoMapper;
using WebAPI.Models;
using WebAPI.Shared.DataTransferObjects;

namespace WebAPI.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DealerShop, DealerShopCreationDTO>().ReverseMap()
                .ForMember(m => m.Location, opt => opt.Ignore());
            CreateMap<DealerShop, DealerShopDTO>().ReverseMap()
                .ForMember(m => m.DealerShopId, o => o.Ignore())
                .ForMember(m => m.Location, opt => opt.Ignore());
        }
    }
}
