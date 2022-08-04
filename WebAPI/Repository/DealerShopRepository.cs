using Entities.Models;
using Repository;
using WebAPI.Interfaces;

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

        public bool DealerShopExists(Guid id)
        {
            var dealerShop = _context.DealerShops.FirstOrDefault(d => d.DealerShopId == id);

            if (dealerShop == null)
                return false;

            return true;
        }

        public DealerShop GetDealerShop(Guid id)
        {
            return _context.DealerShops.FirstOrDefault(d => d.DealerShopId == id);
        }

        public void Add(DealerShop dealerShop)
        {
            _context.DealerShops.Add(dealerShop);
        }
        
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Delete(DealerShop dealerShop)
        {
            _context.DealerShops.Remove(dealerShop);
        }

        public ICollection<PhotoForDealerShop> GetPhotoOfAnDealerShop(Guid id)
        {
            return _context.DealerShops.Where(d => d.DealerShopId == id).FirstOrDefault().Photos;
        }

        public void Update(DealerShop dealerShop)
        {
            _context.DealerShops.Update(dealerShop);
        }

        public bool DealerShopExistsByOrdinalNumber(int ordinalNumber, Guid? dealerShopId = null)
        {
            if(dealerShopId != null)
                return _context.DealerShops.Any(d => d.OrdinalNumber == ordinalNumber && d.DealerShopId != dealerShopId);
            
            return _context.DealerShops.Any(d => d.OrdinalNumber == ordinalNumber);
        }
    }
}
