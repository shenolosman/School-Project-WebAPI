namespace GeoComment.Models
{
    public class MyGeoComment
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public string Author { get; set; }
        public int Latitude { get; set; }
        public int Longitude { get; set; }
    }
}
