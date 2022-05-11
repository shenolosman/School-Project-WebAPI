using Microsoft.AspNetCore.Identity;

namespace GeoComment.Models
{
    public class User : IdentityUser
    {
        public ICollection<MyGeoComment>? MyGeoComments { get; set; }
    }
}
