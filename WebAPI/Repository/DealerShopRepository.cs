using WebAPI.Data;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public class DealerShopRepository : IDealerShopRepository
    {
        private readonly CarDealershipContext _context;

        public DealerShopRepository(CarDealershipContext context)
        {
            _context = context;
        }

        public ICollection<DealerShop> GetDealerShops()
        {
            return _context.DealerShops.OrderBy(d => d.OrdinalNumber).ToList();
        }
    }
}
