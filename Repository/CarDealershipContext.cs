using Microsoft.EntityFrameworkCore;
using Entities.Models;

namespace Repository
{
    public class CarDealershipContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<DealerShop> DealerShops { get; set; }
        public DbSet<PhotoForCar> PhotosForCar { get; set; }
        public DbSet<PhotoForDealerShop> PhotosForDealershop { get; set; }
        public CarDealershipContext(DbContextOptions<CarDealershipContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PhotoForCar>()
                .Property(e => e.Size)
                .HasPrecision(30, 10);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PhotoForDealerShop>()
                .Property(e => e.Size)
                .HasPrecision(30, 10);
            base.OnModelCreating(modelBuilder);
        }

    }
}
