using bsc_be.DTOs;

namespace bsc_be.Services
{
    public interface ITransactionService
    {
        public Task<TransactionResponse[]?> GetTransactionAsync(int userId);

        public Task<Boolean> CreateTransactionAsync(long UserId, TransactionRequest request);

        public Task<Boolean> UpdateTransactionItemAsync(TransactionItemUpdateRequest request);

        public Task<Boolean> DeleteTransactionAsync(long transactionId);
    }
}
