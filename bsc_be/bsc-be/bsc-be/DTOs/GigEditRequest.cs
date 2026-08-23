namespace bsc_be.DTOs
{
    public class GigEditRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Duration { get; set; } = 0;
        public decimal Price { get; set; } = 0;
        public List<long> Types { get; set; } = new List<long>();
    }
}