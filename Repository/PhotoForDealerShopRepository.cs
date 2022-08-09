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
    public class PhotoForDealerShopRepository : RepositoryBase<PhotoForDealerShop>, IPhotoForDealerShopRepository

    {
        public PhotoForDealerShopRepository(CarDealershipContext context) : base(context)
        {
        }

        public async Task<IEnumerable<PhotoForDealerShop>> GetPhotoForDealerShopsAsync(Guid dealerShopId, bool trackChanges) =>
            await FindByCondition(p => p.DealerShopId.Equals(dealerShopId), trackChanges).ToListAsync();

        public async Task<PhotoForDealerShop> GetPhotoForDealerShopByIdAsync(Guid photoId, bool trackChanges) =>
            await FindByCondition(p => p.PhotoId.Equals(photoId), trackChanges).SingleOrDefaultAsync();

        public void DeletePhotoForDealerShop(PhotoForDealerShop photo) => Delete(photo);
    }
}
