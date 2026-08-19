using System.ComponentModel.DataAnnotations.Schema;

namespace bsc_be.Models
{
    public class Transaction
    {
        public long Id { get; set; } = 0;
        [ForeignKey("User")]
        public long BuyerId { get; set; } = 0;
        [ForeignKey("Gig")]
        public long GigId { get; set; } = 0;
        [ForeignKey("Item")]
        public long ItemId { get; set; } = 0;
        [ForeignKey("Rating")]
        public long RatingId { get; set; } = 0;
        [ForeignKey("PaymentMethod")]
        public long PaymentMethodId { get; set; } = 0;
        public decimal TotalPrice { get; set; } = 0;
        public DateTime date { get; set; } = DateTime.Now;
        public string status { get; set; } = string.Empty;
    }
}
