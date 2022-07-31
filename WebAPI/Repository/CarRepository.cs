using WebAPI.Data;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly CarDealershipContext _context;

        public CarRepository(CarDealershipContext context)
        {
            _context = context;
        }
        public ICollection<Car> GetCarsByDealerShop(Guid dealerShopId)
        {
            return _context.Cars.Where(c => c.DealerShopId == dealerShopId).ToList();
        }
    }
}
