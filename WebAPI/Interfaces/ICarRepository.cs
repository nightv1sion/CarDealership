using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface ICarRepository
    {
        ICollection<Car> GetCars();
        void Add(Car car);
        Task SaveAsync();
        ICollection<Car> GetCarsByDealerShop(Guid dealerShopId);
    }
}
