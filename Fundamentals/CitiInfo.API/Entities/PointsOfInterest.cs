﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CitiInfo.API.Entities
{
    public class PointsOfInterest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [ForeignKey("CityId")]
        public int CityId { get; set; }
        public City City { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
    }
}