using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public class CarForManipulationDto
    {
        [Required(ErrorMessage = "Car Licence Plates is required field")]
        public string? LicencePlates { get; set; }
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
        [Required(ErrorMessage = "Car Body Type is required field")]
        public string? BodyType { get; set; }
        [Required(ErrorMessage = "Car Modification is required field")]
        public string? Modification { get; set; }
        [Required(ErrorMessage = "Car Transmission is required field")]
        public string? Transmission { get; set; }
        [Required(ErrorMessage = "Car Drive is required field")]
        public string? Drive { get; set; }
        [Required(ErrorMessage = "Car Engine Type is required field")]
        public string? EngineType { get; set; }
        [Required(ErrorMessage = "Car Color is required field")]
        public string? Color { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Car Dealer Shop Ordinal Number must be above than 0")]
        public int DealerShopOrdinalNumber { get; set; }
    }
}
