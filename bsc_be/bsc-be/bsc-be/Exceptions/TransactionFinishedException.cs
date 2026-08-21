namespace bsc_be.Exceptions
{
    public class TransactionFinishedException:Exception
    {
        public TransactionFinishedException(string transactionId):base($"Transaction with ID {transactionId} is already Finished")
        {
            
        }
    }
}