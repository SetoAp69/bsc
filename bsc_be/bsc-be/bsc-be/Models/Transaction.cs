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

        [Column("RATTING_ID")]
        [ForeignKey(nameof(Models.Rating))]
        public long RatingId { get; set; } = 0;

        [Column("PAYMENT_METHOD")]
        [ForeignKey(nameof(PaymentMethod))]
        public long PaymentMethodId { get; set; } = 0;

        [Column("TOTAL_PRICE")]
        public decimal TotalPrice { get; set; } = 0;
        public DateTime Date { get; set; } = DateTime.Now;

        public Rating? Rating = null;
        public PaymentMethod? PaymentMethod = null;
        public Status Status { get; set; } = Status.ON_PROGRESS;
        public Gig? Gig = null;
        public Item? Item = null;
    }
}
