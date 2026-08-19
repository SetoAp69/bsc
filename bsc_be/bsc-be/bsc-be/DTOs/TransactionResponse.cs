namespace bsc_be.DTOs
{
    public class TransactionResponse
    {
        public long Id { get; set; }
        public string GigName { get; set; } = string.Empty;
        public string TransactionStatus { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; }
    }
}
