using GeoComment.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GeoComment.Data
{
    public class GeoDbContext : IdentityDbContext<User>
    {
        public GeoDbContext(DbContextOptions option) : base(option)
        {

        }
        public DbSet<MyGeoComment> GeoComments { get; set; }
    }
}
