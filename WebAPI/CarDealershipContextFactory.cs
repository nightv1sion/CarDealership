using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repository;

namespace WebAPI
{
    public class CarDealershipContextFactory : IDesignTimeDbContextFactory<CarDealershipContext>
    {
        public CarDealershipContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json")
                 .Build();

            var builder = new DbContextOptionsBuilder<CarDealershipContext>()
                .UseSqlServer(configuration.GetConnectionString("CarDealershipConnection"), b => b.MigrationsAssembly("WebAPI"));
            return new CarDealershipContext(builder.Options);

        }
    }
}
