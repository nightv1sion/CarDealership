using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Repository;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class PhotoForDealerShopService : IPhotoForDealerShopService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public PhotoForDealerShopService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PhotoDTO>> GetPhotosAsync(Guid dealerShopId, bool trackChanges)
        {
            var dealerShop = await _repository.DealerShop.GetDealerShopAsync(dealerShopId, trackChanges);
            if (dealerShop is null)
                throw new DealerShopNotFoundException(dealerShopId);

            var photos = await _repository.PhotoForDealerShop.GetPhotoForDealerShopsAsync(dealerShopId, trackChanges);

            var photosForReturn = _mapper.Map<IEnumerable<PhotoDTO>>(photos);
            return photosForReturn;
        }

        public async Task<PhotoDTO> GetPhotoByIdAsync(Guid photoId, bool trackChanges)
        {
            var photo = await _repository.PhotoForDealerShop.GetPhotoForDealerShopByIdAsync(photoId, trackChanges);
            if (photo is null)
                throw new PhotoForDealerShopNotFoundException(photoId);

            var photoForReturn = _mapper.Map<PhotoDTO>(photo);
            return photoForReturn;
        }

        public async Task DeletePhotoAsync(Guid photoId)
        {
            var photo = await _repository.PhotoForDealerShop.GetPhotoForDealerShopByIdAsync(photoId, false);
            if (photo is null)
                throw new PhotoForDealerShopNotFoundException(photoId);

            _repository.PhotoForDealerShop.DeletePhotoForDealerShop(photo);
            await _repository.SaveAsync();
        }
    }
}
