namespace GeoComment.DTO.v1
{
    public class ResultDto
    {
        public int id { get; set; }
        public string author { get; set; }
        public string message { get; set; }
        public int longitude { get; set; }
        public int latitude { get; set; }
    }
}
