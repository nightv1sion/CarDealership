using Contracts;
using Entities.Models;
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

        public IEnumerable<DealerShop> GetAllDealerShops(bool trackChanges) => FindAll(trackChanges).OrderBy(d => d.OrdinalNumber).ToList();
    }
}
