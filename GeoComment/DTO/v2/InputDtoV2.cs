namespace GeoComment.DTO.v2
{
    public class InputDtoV2
    {
        public int id { get; set; }
        public int longitude { get; set; }
        public int latitude { get; set; }
        public InputBody body { get; set; }
    }

    public class InputBody
    {
        public string? title { get; set; }
        public string message { get; set; }
        public string? author { get; set; }
    }
}
