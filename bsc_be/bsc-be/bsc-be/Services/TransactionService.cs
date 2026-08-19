using bsc_be.DTOs;
using bsc_be.Models;
using bsc_be.Repositories;

namespace bsc_be.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<Transaction> _transactionRepository;
        private readonly IConfiguration _configuration;
        public TransactionService(IRepository<Transaction> transactionRepository, IConfiguration configuration)
        {
            _transactionRepository = transactionRepository;
            _configuration = configuration;
        }
        public async Task<TransactionResponse[]?> GetTransactionAsync(int userId)
        {
            var transactions = await _transactionRepository
                .GetAllAsync("Gig");
            if (transactions == null)
            {
                return null;
            }
            transactions = transactions.Where(t => t.BuyerId == userId).ToList();
            return transactions.Select(userTransaction => new TransactionResponse
            {
                Id = userTransaction.Id,
                GigName = userTransaction.Gig?.Name,
                TransactionStatus = userTransaction.Status.ToString(),
                Date = userTransaction.Date,
                TotalPrice = userTransaction.TotalPrice
            }).ToArray();
        }

        public async Task<TransactionResponse> CreateTransactionAsync(TransactionRequest request)
        {
            var transaction = new Transaction
            {
                BuyerId = request.UserId,
                GigId = request.GigId,
                ItemId = request.ItemId,
                Status = Status.ON_PROGRESS,
                Date = DateTime.UtcNow,
                TotalPrice = request.TotalPrice,
                PaymentMethodId = request.PaymentMethodId,
                Rating = new Rating
                {
                    Star = 0,
                    Comment = string.Empty
                }
            };
            await _transactionRepository.BeginTransactionAsync();
            try
            {
                await _transactionRepository.AddAsync(transaction);
                await _transactionRepository.SaveChangesAsync();
                await _transactionRepository.CommitTransactionAsync();
                return new TransactionResponse
                {
                    Id = transaction.Id,
                    GigName = transaction.Gig?.Name ?? string.Empty,
                    TransactionStatus = transaction.Status.ToString(),
                    Date = transaction.Date,
                    TotalPrice = transaction.TotalPrice
                };
            }
            catch (Exception)
            {
                await _transactionRepository.RollbackTransactionAsync();
                throw;
            }

        }
        public async Task<TransactionResponse> UpdateTransactionStatusAsync(TransactionStatusRequest request)
        {
            var transaction = await _transactionRepository.GetByIdAsync(request.TransactionId);
            if (transaction == null)
            {
                throw new Exception("Transaction not found");
            }
            transaction.Status = Enum.Parse<Status>(request.Status);
            await _transactionRepository.SaveChangesAsync();
            return new TransactionResponse
            {
                Id = transaction.Id,
                GigName = transaction.Gig?.Name ?? string.Empty,
                TransactionStatus = transaction.Status.ToString(),
                Date = transaction.Date,
                TotalPrice = transaction.TotalPrice
            };
        }
    }
}
