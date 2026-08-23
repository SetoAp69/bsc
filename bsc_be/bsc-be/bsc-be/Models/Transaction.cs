using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bsc_be.Models
{
    [Table("TRANSACTION")]
    public class Transaction
    {
        [Key]
        public long Id { get; set; } = 0;

        [Column("BUYER_ID")]
        [ForeignKey(nameof(User))]
        public long BuyerId { get; set; } = 0;

        [Column("GIG_ID")]
        [ForeignKey(nameof(Models.Gig))]
        public long GigId { get; set; } = 0;
        [Column("ITEM_ID")]
        [ForeignKey(nameof(Item))]
        public long? ItemId { get; set; } = null;

        [Column("RATING_ID")]
        [ForeignKey(nameof(Models.Rating))]
        public long RatingId { get; set; } = 0;

        [Column("PAYMENT_METHOD_ID")]
        [ForeignKey(nameof(PaymentMethod))]
        public long PaymentMethodId { get; set; } = 0;

        [Column("BASE_PRICE")]
        public decimal BasePrice {get;set;} = 0;
        [Column("TOTAL_PRICE_PAID")]
        public decimal TotalPricePaid {get;set;} = 0;

        [Column("TOTAL_PRICE_RECEIVED")]
        public decimal TotalPriceReceived { get; set; } = 0;

        [Column("DEADLINE")]
        public DateTime Deadline { get; set; } = DateTime.Now;

        [Column("BUYER_DESCRIPTION")]
        public string BuyerDescription { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
        public User? User { get; set; }

        public Rating? Rating { get; set; } = null;
        public PaymentMethod? PaymentMethod { get; set; } = null;
        public Status Status { get; set; } = Status.IN_PROGRESS;
        public Gig? Gig { get; set; }
        public Item? Item { get; set; } = null;
    }
}
