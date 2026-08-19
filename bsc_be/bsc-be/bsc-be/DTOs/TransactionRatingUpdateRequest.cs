namespace bsc_be.DTOs
{
    public class TransactionRatingUpdateRequest
    {
        public long RatingId { get; set; }
        public decimal StarRating { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}
