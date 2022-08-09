using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CarService : ICarService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CarService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CarDTO>> GetCarsForDealerShopAsync(Guid dealerSHopId, bool trackChanges)
        {
            await CheckDealerShopIfExists(dealerSHopId, trackChanges);
            var carEntities = await _repository.Car.GetCarsForDealerShopAsync(dealerSHopId, trackChanges);

            var carsForReturn = _mapper.Map<IEnumerable<CarDTO>>(carEntities);
            return carsForReturn;
        }

        public async Task<CarDTO> CreateCarAsync(Guid dealerShopId, CarForCreationDTO car)
        {
            await CheckDealerShopIfExists(dealerShopId, false);
            var carForCreate = _mapper.Map<Car>(car);

            _repository.Car.CreateCarForDealerShop(dealerShopId, carForCreate);
            await _repository.SaveAsync();
            var carForReturn = _mapper.Map<CarDTO>(carForCreate);
            return carForReturn;
        }

        public async Task<CarDTO> GetCarForDealerShopByIdAsync(Guid dealerShopId, Guid id, bool trackChanges)
        {
            await CheckDealerShopIfExists(dealerShopId, trackChanges);
            var car = await GetCarAndCheckIfItExists(dealerShopId, id, trackChanges);

            var carForReturn = _mapper.Map<CarDTO>(car);
            return carForReturn;
        }

        public async Task DeleteCarForDealerShop(Guid dealerShopId, Guid id, bool trackChanges)
        {
            await CheckDealerShopIfExists(dealerShopId, trackChanges);
            var car = await GetCarAndCheckIfItExists(dealerShopId, id, trackChanges);
            _repository.Car.DeleteCarForDealerShop(car);
            await _repository.SaveAsync();
        }

        private async Task CheckDealerShopIfExists(Guid dealerShopId, bool trackChanges)
        {
            var dealerShop = await _repository.DealerShop.GetDealerShopAsync(dealerShopId, trackChanges);
            if (dealerShop is null)
                throw new DealerShopNotFoundException(dealerShopId);
        }

        private async Task<Car> GetCarAndCheckIfItExists(Guid dealerShopId, Guid id, bool trackChanges)
        {
            var car = await _repository.Car.GetCarForDealerShopAsync(dealerShopId, id, trackChanges);
            if (car is null)
                throw new CarNotFoundException(id);
            return car;
        }
    }
}
