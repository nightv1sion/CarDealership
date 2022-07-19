using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class Photo
    {
        [Key]
        public Guid PhotoId { get; set; }
        public byte[] Bytes { get; set; }
        public string Description { get; set; }
        public string FileExtension { get; set; }
        public decimal Size { get; set; }
        public Guid CarId { get; set; }
        [ForeignKey("CarId")]
        public Car Car { get; set; }
    }
}
