using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Shared.DataTransferObjects;
using Shared.RequestParameters;

namespace Service.Contracts
{
    public interface IDealerShopService
    {
        Task<IEnumerable<DealerShopDTO>> GetAllDealerShopsAsync(DealerShopParameters dealerShopParameters, bool trackChanges);
        Task<DealerShopDTO> GetDealerShopAsync(Guid id, bool trackChanges);
        Task DeleteDealerShopAsync(Guid id);
        Task<DealerShopDTO> CreateDealerShopAsync(DealerShopForCreationDTO dealerShopCreationDto, List<IFormFile> files);
        Task<DealerShopDTO> UpdateDealerShopAsync(Guid id, DealerShopForUpdateDTO dealerShopForUpdateDto);
        Task<IEnumerable<DealerShopMiniDTO>> GetOrdinalNumbersAsync();

    }
}
