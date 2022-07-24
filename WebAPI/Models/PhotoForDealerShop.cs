using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class PhotoForDealerShop
    {
        [Key]
        public Guid PhotoId { get; set; }
        public byte[] Bytes { get; set; }
        public string Description { get; set; }
        public decimal Size { get; set; }
        public Guid DealerShopId { get; set; }

        [ForeignKey("DealerShopId")]
        public DealerShop DealerShop { get; set; }
    }
}
