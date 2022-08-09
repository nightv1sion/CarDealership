﻿using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface ICarService
    {
        Task<IEnumerable<CarDTO>> GetCarsForDealerShopAsync(Guid dealerSHopId, bool trackChanges);
        Task<CarDTO> CreateCarAsync(Guid dealerShopId, CarForCreationDTO car);
        Task<CarDTO> GetCarForDealerShopByIdAsync(Guid dealerShopId, Guid id, bool trackChanges);
        Task DeleteCarForDealerShop(Guid dealerShopId, Guid id, bool trackChanges);
    }
}