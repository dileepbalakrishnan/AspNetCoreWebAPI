using System.Collections.Generic;
using CitiInfo.API.Models;

namespace CitiInfo.API
{
    public class CitiesDataStore
    {
        public CitiesDataStore()
        {
            Cities = new List<CitiDto>
            {
                new CitiDto
                {
                    Id = 1,
                    Name = "Bangalore",
                    Description = "The Garden City",
                    PointsOfInterest = new List<PointOfInterestDto>
                    {
                        new PointOfInterestDto {Id = 1, Name = "Vidhan Soudha", Description = "The secratariate"},
                        new PointOfInterestDto {Id = 2, Name = "Lal Bag", Description = "The big garden"},
                        new PointOfInterestDto {Id = 3, Name = "MG Road", Description = "The shopping destination"}
                    }
                },
                new CitiDto
                {
                    Id = 2,
                    Name = "Chennai",
                    Description = "The Old Madras",
                    PointsOfInterest = new List<PointOfInterestDto>
                    {
                        new PointOfInterestDto {Id = 1, Name = "Marina Beach", Description = "The long beach"},
                        new PointOfInterestDto {Id = 2, Name = "Ritchie Street", Description = "All about electronics"},
                        new PointOfInterestDto {Id = 3, Name = "Kodambakkam", Description = "The movie makers hub"}
                    }
                },
                new CitiDto
                {
                    Id = 3,
                    Name = "Kochin",
                    Description = "The Trade Capital Of Kerala",
                    PointsOfInterest = new List<PointOfInterestDto>
                    {
                        new PointOfInterestDto {Id = 1, Name = "Naval Base", Description = "The Indian navi space"},
                        new PointOfInterestDto {Id = 2, Name = "Nedumbasseri", Description = "The CIAL"},
                        new PointOfInterestDto {Id = 3, Name = "Infopark", Description = "The IT hub"}
                    }
                }
            };
        }

        public static CitiesDataStore Current { get; } = new CitiesDataStore();
        public List<CitiDto> Cities { get; set; }
    }
}