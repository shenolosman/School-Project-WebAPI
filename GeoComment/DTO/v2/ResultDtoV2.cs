namespace GeoComment.DTO.v2
{
    public class ResultDtoV2
    {
        public int id { get; set; }
        public int longitude { get; set; }
        public int latitude { get; set; }
        public ResultBody body { get; set; }
    }

    public class ResultBody
    {
        public string? title { get; set; }
        public string message { get; set; }
        public string author { get; set; }
    }
}
