using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    public class CarDealershipContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<DealerShop> DealerShops { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public CarDealershipContext(DbContextOptions<CarDealershipContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Photo>()
                .Property(e => e.Size)
                .HasPrecision(12, 10);
            base.OnModelCreating(modelBuilder);
        }
    }
}
