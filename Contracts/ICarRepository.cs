using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetCarsForDealerShopAsync(Guid dealerShopId, bool trackChanges);
        void CreateCarForDealerShop(Guid dealerShopId, Car car);
        Task<Car> GetCarForDealerShopAsync(Guid dealerShopId, Guid id, bool trackChanges);
        void DeleteCarForDealerShop(Car car);
    }
}
