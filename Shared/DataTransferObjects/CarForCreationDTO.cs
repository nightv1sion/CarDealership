using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class CarForCreationDTO
    {
        [Required(ErrorMessage = "Car brand is required field")]
        public string? Brand { get; set; }
        [Required(ErrorMessage = "Car Model is required field")]
        public string? Model { get; set; }
        [Required(ErrorMessage = "Car Country is required field")]
        public string? Country { get; set; }
        [Range(1900, 2022, ErrorMessage = "Car Production year is required and must be above than 1900 and below or equal current year")]
        public int ProductionYear { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Car number of owners is required and must be more than 0")]
        public int NumberOfOwners { get; set; }
    }
}
