using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    public class CarDealershipContext : DbContext
    {
        public IEnumerable<Car> Cars { get; set; }
        public IEnumerable<DealerShop> DealerShops { get; set; }
        public IEnumerable<Photo> Photos { get; set; }
        public CarDealershipContext(DbContextOptions<CarDealershipContext> options) : base(options) { }
    }
}
