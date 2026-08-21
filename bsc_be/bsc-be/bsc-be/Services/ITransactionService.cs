using bsc_be.DTOs;
using bsc_be.Models;

namespace bsc_be.Services
{
    public interface ITransactionService
    {
        public Task<TransactionResponse[]?> GetTransactionsAsync(long userId, UserRole userRole);

        public Task<TransactionResponse?> GetTransactionByIdAsync(long transactionId);
      

        public Task<Boolean> CreateTransactionAsync(long UserId, TransactionRequest request);

        public Task<Boolean> UpdateTransactionItemAsync(TransactionItemUpdateRequest request);

        public Task<Boolean> DeleteTransactionAsync(long transactionId);
    }
}
