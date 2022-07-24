using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;
using WebAPI.Models;

namespace WebAPI.Shared.DataTransferObjects
{
    public class DealerShopCreationDTO
    {
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int OrdinalNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
