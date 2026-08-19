namespace bsc_be.DTOs
{
    public class RatingResponse
    {
        public long Id { get; set; }
        public decimal Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}
