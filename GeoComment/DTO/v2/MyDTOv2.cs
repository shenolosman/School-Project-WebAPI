namespace GeoComment.DTO.v2
{
    public class MyDTOv2
    {
        public int id { get; set; }
        public int longitude { get; set; }
        public int latitude { get; set; }
        public body body { get; set; }
    }

    public class body
    {
        public string Title { get; set; }
        public string message { get; set; }
        public string author { get; set; }
        public List<MyDTOv2> MyDtOv2s { get; set; }
    }
}
