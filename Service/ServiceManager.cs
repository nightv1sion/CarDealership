﻿using AutoMapper;
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
        private readonly Lazy<IPhotoForDealerShopService> _photoForDealerShopService;
        public ServiceManager(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _carService = new Lazy<ICarService>(() => new CarService(repository, logger, mapper));
            _dealerShopService = new Lazy<IDealerShopService>(() => new DealerShopService(repository, logger, mapper));
            _photoForDealerShopService = new(() => new PhotoForDealerShopService(repository, logger, mapper));
        }

        public IDealerShopService DealerShopService => _dealerShopService.Value;
        public ICarService CarService => _carService.Value;
        public IPhotoForDealerShopService PhotoForDealerShopService => _photoForDealerShopService.Value; 

    }
}