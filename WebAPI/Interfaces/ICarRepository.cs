using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface ICarRepository
    {
        ICollection<Car> GetCarsByDealerShop(Guid dealerShopId);
    }
}
