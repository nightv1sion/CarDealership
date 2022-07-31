using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IDealerShopRepository
    {
        ICollection<DealerShop> GetDealerShops();
        bool DealerShopExists(Guid id);
        bool DealerShopExistsByOrdinalNumber(int ordinalNumber, Guid? dealerShopId = null);
        DealerShop GetDealerShop(Guid id);
        void Add(DealerShop dealerShop);
        Task SaveAsync();
        void Delete(DealerShop dealerShop);
        ICollection<PhotoForDealerShop> GetPhotoOfAnDealerShop(Guid id);
        void Update(DealerShop dealerShop);
    }
}
