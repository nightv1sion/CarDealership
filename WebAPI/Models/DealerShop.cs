using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class DealerShop
    {
        [Key]
        public Guid DealerShopId { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int OrdinalNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Point Location { get; set; }
        public List<PhotoForDealerShop> Photos { get; set; }
        public List<Car> Cars { get; set; }

    }
}
