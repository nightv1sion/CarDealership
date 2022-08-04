using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Car
    {
        [Key]
        public Guid CarId { get; set; }
        [Required(ErrorMessage = "Car Brand is required")]
        public string? Brand { get; set; }
        [Required(ErrorMessage = "Car Model is required")]
        public string? Model { get; set; }
        [Required(ErrorMessage = "Car Country is required")]
        public string? Country { get; set; }
        [Required(ErrorMessage = "Car Year Of Production is required")]
        public int ProductionYear { get; set; }
        [Required(ErrorMessage = "Car Number Of Owners is required")]
        public int NumberOfOwners { get; set; }
        public Guid DealerShopId { get; set; }
        public DealerShop DealerShop { get; set; }
        public List<PhotoForCar>? Photos { get; set; }
    }
}
