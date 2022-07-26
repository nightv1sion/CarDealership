﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class PhotoForCar
    {
        [Key]
        public Guid PhotoId { get; set; }
        public byte[] Bytes { get; set; }
        public string Description { get; set; }
        public decimal Size { get; set; }
        public Guid CarId { get; set; }
        [ForeignKey("CarId")]
        public Car Car { get; set; }
    }
}
