using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Car
    {
        [Key]
        public Guid CarId { get; set; }
        [Required(ErrorMessage = "Car Licence Plates is required")]
        public string? LicencePlates { get; set; }
        [Required(ErrorMessage = "Car Brand is required")]
        public string? Brand { get; set; }
        [Required(ErrorMessage = "Car Model is required")]
        public string? Model { get; set; }
        [Required(ErrorMessage = "Car Country is required")]
        public string? Country { get; set; }
        [Required(ErrorMessage = "Car Body Type is required")]
        public string? BodyType { get; set; }
        [Required(ErrorMessage = "Car Modification is required")]
        public string? Modification { get; set; }
        [Required(ErrorMessage = "Car Transmission is required")]
        public string? Transmission { get; set; }
        [Required(ErrorMessage = "Car Drive is required")]
        public string? Drive { get; set; }
        [Required(ErrorMessage = "Car Engine Type is required")]
        public string? EngineType { get; set; }
        [Required(ErrorMessage = "Car Color is required")]
        public string? Color { get; set; }
        [Required(ErrorMessage = "Car Number Of Owners is required")]
        public int NumberOfOwners { get; set; }
        [Required(ErrorMessage = "Car Year Of Production is required")]
        public int ProductionYear { get; set; }
        public Guid DealerShopId { get; set; }
        public DealerShop DealerShop { get; set; }
        public List<PhotoForCar>? Photos { get; set; }
    }
}
