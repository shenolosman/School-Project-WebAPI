
using GeoComment.Data;

namespace GeoComment.Services
{
    public class GeoService
    {
        private readonly GeoDbContext _dbContex;

        public GeoService(GeoDbContext dbContex)
        {
            _dbContex = dbContex;
        }


        public async Task<bool> ResetDB()
        {
            await _dbContex.Database.EnsureDeletedAsync();
            return  await _dbContex.Database.EnsureCreatedAsync();
        }
    }
}
