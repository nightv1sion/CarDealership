using Entities.Models;
using Shared.RequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IDealerShopRepository
    {
        Task<IEnumerable<DealerShop>> GetAllDealerShopsAsync(DealerShopParameters dealerShopParameters, bool trackChanges);
        Task<DealerShop> GetDealerShopAsync(Guid id, bool trackChanges);
        void DeleteDealerShop(DealerShop dealerShop);
        void CreateDealerShop(DealerShop dealerShop);
    }
}
