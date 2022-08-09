using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class DealerShopRepository : RepositoryBase<DealerShop>, IDealerShopRepository
    {
        public DealerShopRepository(CarDealershipContext context) : base(context)
        {
        }

        public async Task<IEnumerable<DealerShop>> GetAllDealerShopsAsync(bool trackChanges) => 
            await FindAll(trackChanges).OrderBy(d => d.OrdinalNumber).ToListAsync();

        public async Task<DealerShop> GetDealerShopAsync(Guid id, bool trackChanges) =>
            await FindByCondition(d => d.DealerShopId.Equals(id), trackChanges)
            .SingleOrDefaultAsync();

        public void DeleteDealerShop(DealerShop dealerShop) => Delete(dealerShop);
        public void CreateDealerShop(DealerShop dealerShop) => Create(dealerShop);

    }
}
