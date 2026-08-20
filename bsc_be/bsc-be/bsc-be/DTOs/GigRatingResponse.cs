namespace bsc_be.DTOs
{
    public class GigRatingResponse()
    {
        public long Id { get; set; } = 0;
        public string userName { get; set; } = string.Empty;
        public decimal Rating { get; set; } = 0.0m;
        public string Comment { get; set; } = string.Empty;
    }
}