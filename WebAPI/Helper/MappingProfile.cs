using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;


namespace WebAPI.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DealerShop, DealerShopForCreationDTO>().ReverseMap()
                .ForMember(m => m.Location, opt => opt.Ignore());

            CreateMap<DealerShop, DealerShopDTO>().ReverseMap()
                .ForMember(m => m.DealerShopId, o => o.Ignore())
                .ForMember(m => m.Location, opt => opt.Ignore());

            CreateMap<DealerShopForUpdateDTO, DealerShop>()
                .ForMember(m => m.Location, o => o.Ignore());

            CreateMap<Car, CarDTO>();

            CreateMap<CarForCreationDTO, Car>()
                .ForMember(c => c.DealerShopId, opt => opt.Ignore());

            CreateMap<PhotoForDealerShop, PhotoDTO>()
                .ForCtorParam("Id", opt => opt.MapFrom(p => p.PhotoId))
                .ForCtorParam("Name", opt => opt.MapFrom(p => p.Description))
                .ForCtorParam("Picture", opt => opt.MapFrom(p => Convert.ToBase64String(p.Bytes)))
                .ForCtorParam("PictureFormat", opt => opt.MapFrom(p => p.PhotoFormat));

            CreateMap<DealerShop, DealerShopMiniDTO>()
                .ForMember(c => c.Id, opt => opt.MapFrom(c => c.DealerShopId));

            CreateMap<CarForEditDto, Car>();
        }
    }
}
