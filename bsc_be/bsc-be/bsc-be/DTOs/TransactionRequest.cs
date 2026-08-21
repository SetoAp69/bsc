using System.ComponentModel.DataAnnotations;

namespace bsc_be.DTOs
{
    public class TransactionRequest
    {
        [Required(ErrorMessage = "UserId is required.")]
        public long UserId { get; set; }
        [Required(ErrorMessage = "GigId is required.")]
        public long GigId { get; set; }
        [Required(ErrorMessage = "ItemId is required.")]
        public string ItemName { get; set; } = string.Empty;
        public string ItemDescription { get; set; } = string.Empty;
        [Required(ErrorMessage = "PaymentMethodId is required.")]
        public long PaymentMethodId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
