using bsc_be.DTOs;

namespace bsc_be.Services
{
    public interface ITransactionService
    {
        public Task<TransactionResponse[]?> GetTransactionAsync(int userId);

        public Task<Boolean> CreateTransactionAsync(TransactionRequest request, long itemId);

        public Task<TransactionResponse> UpdateTransactionStatusAsync(TransactionStatusRequest request);

        public Task<Boolean> DeleteTransactionAsync(long transactionId);
    }
}
