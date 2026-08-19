namespace bsc_be.DTOs
{
    public class GigResponse
    {
        public long Id { get; set; } = 0;
        public string Name { get; set; } =string.Empty;
        public decimal Price { get; set; } = 0;
        public decimal Stars { get; set; } = 0.0m;
        public string GigCreator{get;set;} = string.Empty;
        public List<string> Types{get;set;} = new List<string>();
    }
}