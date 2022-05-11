using GeoComment.Data;
using GeoComment.Models;
using Microsoft.EntityFrameworkCore;

namespace GeoComment.Services
{
    public class GeoService
    {
        private readonly GeoDbContext _dbContext;
        public GeoService(GeoDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<MyGeoComment> Add(MyGeoComment geoComment)
        {
            var newcomment = await _dbContext.AddAsync(geoComment);
            await _dbContext.SaveChangesAsync();
            return newcomment.Entity;
        }
        public async Task<MyGeoComment> GetId(int id)
        {
            return await _dbContext.GeoComments.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<MyGeoComment>> RangeFilter(double? minLon, double? maxLon, double? minLat, double? maxLat)
        {
            return await _dbContext.GeoComments.Include(x => x.User).Where(x =>
                  x.Longitude >= minLon &&
                  x.Longitude <= maxLon &&
                  x.Latitude >= minLat &&
                  x.Latitude <= maxLat).ToListAsync();
        }
        public async Task<MyGeoComment[]> FindByName(string username)
        {
            var result = _dbContext.GeoComments.Include(x => x.User)
                .Where(x => x.User.UserName == username || x.Author == username);
            return await result.ToArrayAsync();
        }
        public async Task ResetDb()
        {
            await _dbContext.Database.EnsureDeletedAsync();
            await _dbContext.Database.EnsureCreatedAsync();
        }
    }
}