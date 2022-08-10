namespace Shared.DataTransferObjects
{
    public class CarDTO
    {
        public Guid CarId { get; set; }
        public string LicencePlates { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Country { get; set; }
        public string BodyType { get; set; }
        public string Modification { get; set; }
        public string Transmission { get; set; }
        public string Drive { get; set; }
        public string EngineType { get; set; }
        public string Color { get; set; }
        public int DealerShopOrdinalNumber { get; set; }
        public int ProductionYear { get; set; }
        public int NumberOfOwners { get; set; }
    }
}
