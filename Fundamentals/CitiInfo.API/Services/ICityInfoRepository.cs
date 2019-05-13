using System.Collections.Generic;
using CitiInfo.API.Entities;

namespace CitiInfo.API.Services
{
    public interface ICityInfoRepository
    {
        IEnumerable<City> GetCities();
        City GetCity(int cityId, bool includePointsOfInterest);
        IEnumerable<PointsOfInterest> GetPointsOfInterestForCity(int cityId);
        PointsOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId);
        bool DoesCityExist(int cityId);
        void AddPointOfInterestForCity(int cityId, PointsOfInterest pointsOfInterest);
        bool Save();
        void DeletePointOfInterest(PointsOfInterest pointsOfInterest);
    }
}