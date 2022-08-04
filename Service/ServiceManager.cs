using AutoMapper;
using Contracts;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICarService> _carService;
        private readonly Lazy<IDealerShopService> _dealerShopService;
        public ServiceManager(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _carService = new Lazy<ICarService>(() => new CarService(repository, logger));
            _dealerShopService = new Lazy<IDealerShopService>(() => new DealerShopService(repository, logger, mapper));
        }

        public IDealerShopService DealerShopService => _dealerShopService.Value;
        public ICarService CarService => _carService.Value;
    }
}
