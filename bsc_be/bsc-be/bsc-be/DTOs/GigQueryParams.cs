namespace bsc_be.DTOs
{
    public class GigQueryParams
    {
        public string? Search { get; set; }
        public int Limit { get; set; }
        public int Page { get; set; }
        public long? UserId { get; set; }
        public List<string> Types { get; set; } = [];

    }
}