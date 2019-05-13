using System.Collections.Generic;
using CitiInfo.API.Entities;
using Microsoft.EntityFrameworkCore.Internal;

namespace CitiInfo.API
{
    public static class CityInfoExtensions
    {
        public static void EnsureSeedDataForContext(this CityInfoContext cityInfoContext)
        {
            if (cityInfoContext.Cities.Any())
            {
                return;
            }
            var cities = new List<City>
            {
                new City
                {
                    Name = "Bangalore",
                    Description = "The Garden City",
                    PointsOfInterest = new List<PointsOfInterest>
                    {
                        new PointsOfInterest {Name = "Vidhan Soudha", Description = "The secratariate"},
                        new PointsOfInterest {Name = "Lal Bag", Description = "The big garden"},
                        new PointsOfInterest {Name = "MG Road", Description = "The shopping destination"}
                    }
                },
                new City
                {
                    Name = "Chennai",
                    Description = "The Old Madras",
                    PointsOfInterest = new List<PointsOfInterest>
                    {
                        new PointsOfInterest {Name = "Marina Beach", Description = "The long beach"},
                        new PointsOfInterest {Name = "Ritchie Street", Description = "All about electronics"},
                        new PointsOfInterest {Name = "Kodambakkam", Description = "The movie makers hub"}
                    }
                },
                new City
                {
                    Name = "Kochin",
                    Description = "The Trade Capital Of Kerala",
                    PointsOfInterest = new List<PointsOfInterest>
                    {
                        new PointsOfInterest {Name = "Naval Base", Description = "The Indian navi space"},
                        new PointsOfInterest {Name = "Nedumbasseri", Description = "The CIAL"},
                        new PointsOfInterest {Name = "Infopark", Description = "The IT hub"}
                    }
                }
            };
            cityInfoContext.Cities.AddRange(cities);
            cityInfoContext.SaveChanges();
        }
    }
}