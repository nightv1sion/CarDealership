using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using NetTopologySuite.Geometries;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class DealerShopService : IDealerShopService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        
        public DealerShopService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DealerShopDTO>> GetAllDealerShopsAsync(bool trackChanges)
        {
            var dealerShops = await _repository.DealerShop.GetAllDealerShopsAsync(trackChanges);
            var dealerShopsDto = _mapper.Map<IEnumerable<DealerShopDTO>>(dealerShops);
                
            return dealerShopsDto;
        }

        public async Task<DealerShopDTO> GetDealerShopAsync(Guid id, bool trackChanges)
        {
            var dealerShop = await _repository.DealerShop.GetDealerShopAsync(id, trackChanges);
            if (dealerShop is null)
                throw new DealerShopNotFoundException(id);

            var dealerShopDto = _mapper.Map<DealerShopDTO>(dealerShop);
            return dealerShopDto;   
        }

        public async Task DeleteDealerShopAsync(Guid id)
        {
            var dealerShop = await _repository.DealerShop.GetDealerShopAsync(id, false);
            if (dealerShop is null)
                throw new DealerShopNotFoundException(id);

            _repository.DealerShop.DeleteDealerShop(dealerShop);
            await _repository.SaveAsync();
        }
        
        public async Task<DealerShopDTO> CreateDealerShopAsync(DealerShopForCreationDTO dealerShopCreationDto, List<IFormFile> files)
        {
            var dealerShopEntity = _mapper.Map<DealerShop>(dealerShopCreationDto);

            dealerShopEntity.Photos = new List<PhotoForDealerShop>();
            dealerShopEntity.Cars = new List<Car>();
            dealerShopEntity.Location = ConvertLocation(dealerShopCreationDto.Location);

            foreach (var file in files)
            {
                dealerShopEntity.Photos.Add(ConvertFileToPhoto(file, dealerShopEntity));
            }

            _repository.DealerShop.CreateDealerShop(dealerShopEntity);
            await _repository.SaveAsync();

            var dealerShopForReturn = _mapper.Map<DealerShopDTO>(dealerShopEntity);

            return dealerShopForReturn;
        }

        public async Task<DealerShopDTO> UpdateDealerShopAsync(Guid id, DealerShopForUpdateDTO dealerShopForUpdateDto)
        {
            var dealerShopEntity = await _repository.DealerShop.GetDealerShopAsync(id, true);
            if (dealerShopEntity is null)
                throw new DealerShopNotFoundException(id);

            // loading photos is here

            dealerShopEntity.Location = ConvertLocation(dealerShopForUpdateDto.Location);
            
            _mapper.Map(dealerShopForUpdateDto, dealerShopEntity);

            await _repository.SaveAsync();

            var dealerShopForReturn = _mapper.Map<DealerShopDTO>(dealerShopEntity);

            return dealerShopForReturn;
        }

        public async Task<IEnumerable<DealerShopMiniDTO>> GetOrdinalNumbersAsync()
        {
            var dealerShopEntities = await _repository.DealerShop.GetAllDealerShopsAsync(false);
            var result = _mapper.Map<IEnumerable<DealerShopMiniDTO>>(dealerShopEntities);

            return result;
        }


        private PhotoForDealerShop ConvertFileToPhoto(IFormFile file, DealerShop dealerShop)
        {
            var photo = new PhotoForDealerShop()
            {
                PhotoId = Guid.NewGuid(),
                Description = file.FileName,
                Size = file.Length,
                DealerShop = dealerShop,
                DealerShopId = dealerShop.DealerShopId,
                PhotoFormat = file.ContentType
            };
            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    photo.Bytes = fileBytes;
                }

            }
            return photo;
        }

        private Point ConvertLocation(string location)
        {
            var numsOfLocation = location.Split(", ");
            double coordX = double.Parse(numsOfLocation[0], 
                System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo);
            double coordY = double.Parse(numsOfLocation[1],
                System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo);
            return new Point(coordX, coordY) { SRID = 4326 };
        }
    }
}
