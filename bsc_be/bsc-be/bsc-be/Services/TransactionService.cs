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
                .GetAllAsync("Gig", "Rating", "Item");
            if (transactions == null)
            {
                return null;
            }
            transactions = transactions.Where(t => t.BuyerId == userId).ToList();
            return transactions.Select(userTransaction => new TransactionResponse
            {
                Id = userTransaction.Id,
                GigName = userTransaction.Gig.Name,
                TransactionStatus = userTransaction.Status.ToString(),
                Date = userTransaction.Date,
                TotalPrice = userTransaction.TotalPrice,
                BuyerDescription = userTransaction.BuyerDescription,
                Rating = new RatingResponse
                {
                    Id = userTransaction.Rating.Id,
                    Rating = userTransaction.Rating.Star,
                    Comment = userTransaction.Rating.Comment
                },
                Item = new ItemResponse
                {
                    Id = userTransaction.Item.Id,
                    Name = userTransaction.Item.Name,
                    Path = userTransaction.Item.Path
                }
            }).ToArray();
        }

        public async Task<Boolean> CreateTransactionAsync(TransactionRequest request)
        {
            var transaction = new Transaction
            {
                BuyerId = request.UserId,
                GigId = request.GigId,
                Item = new Item
                {
                    Name = string.Empty,
                    Description = string.Empty,
                    Path = string.Empty
                },
                Status = Status.IN_PROGRESS,
                Date = DateTime.UtcNow,
                TotalPrice = request.TotalPrice,
                PaymentMethodId = request.PaymentMethodId,
                Rating = new Rating
                {
                    Star = 0,
                    Comment = string.Empty
                },
                BuyerDescription = request.BuyerDescription
            };
            await _transactionRepository.BeginTransactionAsync();
            try
            {
                await _transactionRepository.AddAsync(transaction);
                await _transactionRepository.SaveChangesAsync();
                await _transactionRepository.CommitTransactionAsync();
                return true;
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
                TotalPrice = transaction.TotalPrice,
                BuyerDescription = transaction.BuyerDescription
            };
        }

        public async Task<Boolean> DeleteTransactionAsync(long transactionId)
        {
            var transaction = await _transactionRepository.GetByIdAsync(transactionId);
            if (transaction == null) return false;
            await _transactionRepository.BeginTransactionAsync();
            try
            {
                transaction.Status = Status.CANCELED;
                _transactionRepository.Update(transaction);
                await _transactionRepository.SaveChangesAsync();
                await _transactionRepository.CommitTransactionAsync();
                return true;
            }
            catch
            {
                await _transactionRepository.RollbackTransactionAsync();
                return false;
            }

        }
    }
}
