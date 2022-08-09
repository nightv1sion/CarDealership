using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public abstract record DealerShopForManipulationDTO
    {
        [Required(ErrorMessage = "Dealer shop address is required")]
        public string? Address { get; init; }
        [Required(ErrorMessage = "Dealer shop address is required")]
        public string Country { get; init; }
        [Required(ErrorMessage = "Dealer shop address is required")]
        public string City { get; init; }
        public int OrdinalNumber { get; init; }
        [Required(ErrorMessage = "Dealer shop address is required")]
        public string Email { get; init; }
        [Phone(ErrorMessage = "Dealer shop phone number is required and must in right format")]
        public string PhoneNumber { get; init; }
        [Required(ErrorMessage = "Dealer shop location is required")]
        public string Location { get; init; }
        public List<IFormFile>? Files { get; init; }
    }
}
