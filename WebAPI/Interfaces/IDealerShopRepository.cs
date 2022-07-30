using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IDealerShopRepository
    {
        ICollection<DealerShop> GetDealerShops();
    }
}
