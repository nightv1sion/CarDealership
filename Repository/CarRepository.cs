using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CarRepository : RepositoryBase<Car>, ICarRepository
    {
        public CarRepository(CarDealershipContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Car>> GetCarsForDealerShopAsync(Guid dealerShopId, bool trackChanges) =>
            await FindByCondition(c => c.DealerShopId.Equals(dealerShopId), trackChanges).OrderBy(c => c.Model).ToListAsync();

        public async Task<Car> GetCarForDealerShopAsync(Guid dealerShopId, Guid id, bool trackChanges) => 
            await FindByCondition(c => c.DealerShopId.Equals(dealerShopId) && c.CarId.Equals(id), trackChanges).OrderBy(c => c.Model).SingleOrDefaultAsync();
        public void CreateCarForDealerShop(Guid dealerShopId, Car car)
        {
            car.DealerShopId = dealerShopId;
            Create(car);
        }

        public void DeleteCarForDealerShop(Car car) => Delete(car);
    }
}
