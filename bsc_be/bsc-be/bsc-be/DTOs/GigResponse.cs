namespace bsc_be.DTOs
{
    public class GigResponse
    {
        public long Id { get; set; } = 0;
        public string Name { get; set; } =string.Empty;
        public decimal Price { get; set; } = 0;
        public decimal Stars { get; set; } = 0.0m;
        public string GigCreator = string.Empty;
        public List<string> Types = new List<string>();
    }
}