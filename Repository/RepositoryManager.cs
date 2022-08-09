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
        private readonly Lazy<IPhotoForDealerShopRepository> _photoForDealerShopRepository;

        public RepositoryManager(CarDealershipContext context)
        {
            _context = context;
            _carRepository = new Lazy<ICarRepository>(() => new CarRepository(_context));
            _dealerShopRepository = new Lazy<IDealerShopRepository>(() => new DealerShopRepository(_context));
            _photoForDealerShopRepository = new Lazy<IPhotoForDealerShopRepository>(() => new PhotoForDealerShopRepository(_context));
        }

        public ICarRepository Car => _carRepository.Value;
        public IDealerShopRepository DealerShop => _dealerShopRepository.Value;
        public IPhotoForDealerShopRepository PhotoForDealerShop => _photoForDealerShopRepository.Value;
        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
