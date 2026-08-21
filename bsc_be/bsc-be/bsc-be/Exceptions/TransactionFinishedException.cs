namespace bsc_be.Exceptions
{
    public class TransactionFinishedException:Exception
    {
        public TransactionFinishedException(string transactionId, string status):base($"Transaction with ID {transactionId} is already {status}")
        {
            
        }
    }
}