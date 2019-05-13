using System.ComponentModel.DataAnnotations;

namespace CitiInfo.API.Models
{
    public class PointOfInterestForCreationDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}