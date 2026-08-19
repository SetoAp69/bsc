namespace bsc_be.DTOs
{
    public class TransactionStatusRequest
    {
        public long TransactionId { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
