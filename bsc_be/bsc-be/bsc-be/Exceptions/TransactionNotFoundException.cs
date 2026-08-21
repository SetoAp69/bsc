namespace bsc_be.Exceptions
{
    public class TransactionNotFoundException
        :Exception
    {
        public TransactionNotFoundException(string transactionId)
            : base($"Transaction with ID {transactionId} not found")
        {

        }
    }
}
