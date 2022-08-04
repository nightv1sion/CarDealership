using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly CarDealershipContext _context;
        private readonly Lazy<ICarRepository> _carRepository;
        private readonly Lazy<IDealerShopRepository> _dealerShopRepository;

        public RepositoryManager(CarDealershipContext context)
        {
            _context = context;
            _carRepository = new Lazy<ICarRepository>(() => new CarRepository(_context));
            _dealerShopRepository = new Lazy<IDealerShopRepository>(() => new DealerShopRepository(_context));
        }

        public ICarRepository Car => _carRepository.Value;
        public IDealerShopRepository DealerShop => _dealerShopRepository.Value;
        public void Save() => _context.SaveChanges();
    }
}
