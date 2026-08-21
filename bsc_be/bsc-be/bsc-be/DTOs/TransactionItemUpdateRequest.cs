using bsc_be.Models;

namespace bsc_be.DTOs
{
    public class TransactionItemUpdateRequest
    {
        public long Id { get; set; }
        public string TransactionStatus { get; set; } = string.Empty;
        public ItemRequest? Item { get; set; }
    }
}
