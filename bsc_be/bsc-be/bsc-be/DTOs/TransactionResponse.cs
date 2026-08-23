using bsc_be.Models;

namespace bsc_be.DTOs
{
    public class TransactionResponse
    {
        public long Id { get; set; }
        public string GigName { get; set; } = string.Empty;
        public string TransactionStatus { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public decimal TotalPriceReceived { get; set; }
        public decimal TotalPricePaid { get; set; }
        public string BuyerDescription { get; set; } = string.Empty;
        public ItemResponse? Item { get; set; }
        public RatingResponse? Rating { get; set; }
        public DateTime Date { get; set; }
        public DateTime Deadline { get; set;}
    }
}
