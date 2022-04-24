using GeoComment.Data;
using GeoComment.Models;
using Microsoft.EntityFrameworkCore;

namespace GeoComment.Services
{
    public class GeoService
    {
        private readonly GeoDbContext _dbContex;

        public GeoService(GeoDbContext dbContex)
        {
            _dbContex = dbContex;
        }
        public async Task<MyGeoComment> Add(MyDTO geoComment)
        {
            var geo = new MyGeoComment
            {
                Author = geoComment.author,
                Message = geoComment.message,
                Latitude = (int)geoComment.latitude,
                Longitude = (int)geoComment.longitude
            };
            await _dbContex.AddAsync(geo);
            await _dbContex.SaveChangesAsync();
            return geo;
        }
        public async Task<MyDTO> GetId(int id)
        {
            var result = await _dbContex.GeoComments.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                return null;
            }
            var myNewDto = new MyDTO
            {
                id = result.Id,
                author = result.Author,
                message = result.Message,
                longitude = result.Longitude,
                latitude = result.Latitude
            };
            return myNewDto;
        }

        public async Task<List<MyDTO>> RangeFilter(int minlon, int maxlon, int minlat, int maxlat)
        {
            var filter = await _dbContex.GeoComments.Where(x =>
                  x.Longitude >= minlon &&
                  x.Longitude <= maxlon &&
                  x.Latitude >= minlat &&
                  x.Latitude <= maxlat).Select(x => new MyDTO
                  {
                      author = x.Author,
                      message = x.Message,
                      latitude = x.Latitude,
                      longitude = x.Longitude,
                      id = x.Id
                  }).ToListAsync();
            return filter;
        }
        public async Task<bool> ResetDB()
        {
            await _dbContex.Database.EnsureDeletedAsync();
            return await _dbContex.Database.EnsureCreatedAsync();
        }
    }
}
