using System.Collections.Generic;

namespace CitiInfo.API.Models
{
    public class CitiDto
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public ICollection<PointOfInterestDto> PointsOfInterest { get; set; } = new List<PointOfInterestDto>();
        public int NumberOfPointsOfInterest => PointsOfInterest.Count;
    }
}