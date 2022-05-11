namespace GeoComment.Models
{
    public class InputDto
    {
        public string? author { get; set; }
        public string? message { get; set; }
        public int longitude { get; set; }
        public int latitude { get; set; }
    }
}
