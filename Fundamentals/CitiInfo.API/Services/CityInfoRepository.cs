using System.Collections.Generic;
using System.Linq;
using CitiInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CitiInfo.API.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoContext _cityInfoContext;

        public CityInfoRepository(CityInfoContext cityInfoContext)
        {
            _cityInfoContext = cityInfoContext;
        }

        public IEnumerable<City> GetCities()
        {
            return _cityInfoContext.Cities.OrderBy(c => c.Name).ToList();
        }

        public City GetCity(int cityId, bool includePointsOfInterest)
        {
            if (includePointsOfInterest)
                return _cityInfoContext.Cities.Include(c => c.PointsOfInterest).FirstOrDefault(c => c.Id == cityId);
            return _cityInfoContext.Cities.FirstOrDefault(c => c.Id == cityId);
        }

        public IEnumerable<PointsOfInterest> GetPointsOfInterestForCity(int cityId)
        {
            return _cityInfoContext.PointOfInterest.Where(p => p.CityId == cityId).ToList();
        }

        public PointsOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId)
        {
            return _cityInfoContext.PointOfInterest.FirstOrDefault(p =>
                p.CityId == cityId && p.Id == pointOfInterestId);
        }

        public bool DoesCityExist(int cityId)
        {
            return _cityInfoContext.Cities.Any(c => c.Id == cityId);
        }

        public void AddPointOfInterestForCity(int cityId, PointsOfInterest pointsOfInterest)
        {
            var city = GetCity(cityId, false);
            city.PointsOfInterest.Add(pointsOfInterest);
        }

        public bool Save()
        {
            return _cityInfoContext.SaveChanges() >= 0;
        }

        public void DeletePointOfInterest(PointsOfInterest pointOfInterest)
        {
            _cityInfoContext.PointOfInterest.Remove(pointOfInterest);
        }
    }
}