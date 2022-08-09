using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IPhotoForDealerShopRepository
    {
        Task<IEnumerable<PhotoForDealerShop>> GetPhotoForDealerShopsAsync(Guid dealerShopId, bool trackChanges);
        Task<PhotoForDealerShop> GetPhotoForDealerShopByIdAsync(Guid photoId, bool trackChanges);
        void DeletePhotoForDealerShop(PhotoForDealerShop photo);
    }
}
