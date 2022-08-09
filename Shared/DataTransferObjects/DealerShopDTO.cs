using Microsoft.AspNetCore.Http;

namespace Shared.DataTransferObjects
{
    public record DealerShopDTO(Guid DealerShopId, string Address, string Country, string City, int OrdinalNumber,
        string Email, string PhoneNumber, string Location);
}
