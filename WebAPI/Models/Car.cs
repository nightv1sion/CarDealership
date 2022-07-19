using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Car
    {
        [Key]
        public Guid CarId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Country { get; set; }
        public int ProductionYear { get; set; }
        public int NumberOfOwners { get; set; }
        public DealerShop DealerShop { get; set; }
        public Guid DealerShopId { get; set; }
        public List<Photo> Photos { get; set; }
    }
}
