using System.ComponentModel.DataAnnotations;
namespace GeoComment.Models
{
    public class MyGeoComment
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public string Author { get; set; }
        public int Latitude { get; set; }
        public int Longitude { get; set; }
    }
}
