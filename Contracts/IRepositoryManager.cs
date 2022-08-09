﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        ICarRepository Car { get; }
        IDealerShopRepository DealerShop { get; }
        IPhotoForDealerShopRepository PhotoForDealerShop { get; }
        Task SaveAsync();
    }
}
