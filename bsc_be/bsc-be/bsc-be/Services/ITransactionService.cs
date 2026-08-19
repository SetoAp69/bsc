using bsc_be.DTOs;

namespace bsc_be.Services
{
    public interface ITransactionService
    {
        public Task<TransactionResponse[]?> GetTransactionAsync(int userId);

        public Task<TransactionResponse> CreateTransactionAsync(TransactionRequest request);

        public Task<TransactionResponse> UpdateTransactionStatusAsync(TransactionStatusRequest request);

        public Task<Boolean> DeleteTransactionAsync(long transactionId);
    }
}
