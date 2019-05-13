using Microsoft.EntityFrameworkCore;

namespace CitiInfo.API.Entities
{
    public class CityInfoContext : DbContext
    {
        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options)
        {
            Database.Migrate();
        }
        public DbSet<City> Cities { get; set; }
        public DbSet<PointsOfInterest> PointOfInterest { get; set; }
    }
}