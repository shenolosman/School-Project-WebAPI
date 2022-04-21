using GeoComment.Models;
using Microsoft.EntityFrameworkCore;

namespace GeoComment.Data
{
    public class GeoDbContext : DbContext
    {
        public GeoDbContext(DbContextOptions option) : base(option)
        {

        }
        public DbSet<MyGeoComment> GeoComments { get; set; }
    }
}
