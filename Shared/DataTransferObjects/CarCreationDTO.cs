namespace Shared.DataTransferObjects
{
    public class CarCreationDTO
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Country { get; set; }
        public int ProductionYear { get; set; }
        public int NumberOfOwners { get; set; }
        public string DealerShopId { get; set; }
    }
}
