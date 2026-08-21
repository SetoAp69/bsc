using System.Reflection.Metadata.Ecma335;
using bsc_be.DTOs;
using bsc_be.Models;
using bsc_be.Repositories;

namespace bsc_be.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<Transaction> _transactionRepository;
        private readonly IConfiguration _configuration;
        private readonly IRepository<Item> _itemRepository;
        private readonly ILogger<TransactionService> _logger;
        public TransactionService(
            IRepository<Transaction> transactionRepository,
            IConfiguration configuration,
            IRepository<Item> itemRepository,
            ILogger<TransactionService> logger
            )
        {
            _transactionRepository = transactionRepository;
            _configuration = configuration;
            _itemRepository = itemRepository;
            _logger = logger;
        }
        public async Task<TransactionResponse[]?> GetTransactionsAsync(long userId, UserRole userRole)
        {
            if (userRole == UserRole.CUSTOMER)
            {
                return await GetCustomerTransactionsAsync(userId);
            }
            else
            {
                return await GetServiceProviderTransactionsAsync(userId);
            }
        }


        public async Task<TransactionResponse?> GetTransactionByIdAsync(long transactionId)
        {
            var transaction = await _transactionRepository.GetByIdAsync(transactionId, "Gig", "Rating", "Item");
            if (transaction == null)
            {
                throw new Exception("Transaction not found");
            }
            return toTransactionResponse(transaction);
        }

        public async Task<Boolean> CreateTransactionAsync(long UserId, TransactionRequest request)
        {
            var transaction = new Transaction
            {
                BuyerId = UserId,
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
                _logger.LogWarning("Unknown Error: Adding Transaction failed");
                throw;
            }

        }

        public async Task<Boolean> UpdateTransactionItemAsync(TransactionItemUpdateRequest request)
        {
            var transaction = await _transactionRepository.GetByIdAsync(request.Id, "Gig");
            if (transaction == null)
            {
                _logger.LogWarning("Transaction not found for transaction id {TransactionId}", request.Id);
                return false;
            }
            var itemId = transaction.ItemId;
            await _transactionRepository.BeginTransactionAsync();

            try
            {
                transaction.Status = Enum.Parse<Status>(request.TransactionStatus);
                var item = await _itemRepository.GetByIdAsync(itemId.Value);
                item.Name = request.Item.Name;
                item.Description = request.Item.Description;
                item.Path = request.Item.Path;

                if (request.TransactionStatus == "FINISHED")
                {
                    transaction.TotalPrice = calculateFinalPrice(transaction);
                }

                await _transactionRepository.SaveChangesAsync();
                await _transactionRepository.CommitTransactionAsync();
                return true;
            }
            catch (Exception)
            {
                await _transactionRepository.RollbackTransactionAsync();
                return false;
            }
        }

        private decimal calculateFinalPrice(Transaction transaction)
        {
            var startingDate = transaction.Date;
            var deadline = startingDate.AddDays(transaction.Gig?.Duration ?? 0);
            var overdue = ( DateTime.Now.Date - deadline).Days;

            if (overdue < 0)
            {
                return transaction.TotalPrice;
            }

            var penaltyRate = 0.01m;
            var basePenaltyRate = 0.1m;
            var basePenalty = (transaction?.Gig?.Price ?? 0) * basePenaltyRate;
            var maxPenalty = (double)transaction.TotalPrice * 0.5;
            var penalty = (double)basePenalty * Math.Pow((double)(1 + penaltyRate), overdue);
            if (penalty > maxPenalty)
            {
                penalty = maxPenalty;
            }

            return (transaction?.TotalPrice ?? 0) - (decimal)penalty;
        }


        public async Task<Boolean> DeleteTransactionAsync(long transactionId)
        {
            var transaction = await _transactionRepository.GetByIdAsync(transactionId);
            if (transaction == null)
            {
                _logger.LogWarning("Transaction not found for transaction id {TransactionId}", transaction);
                return false;
            }

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

        private async Task<TransactionResponse[]?> GetCustomerTransactionsAsync(long userId)
        {
            var transactions = await _transactionRepository
              .GetAllAsync("Gig", "Rating", "Item");
            if (transactions == null)
            {
                return null;
            }
            transactions = transactions.Where(t => t.BuyerId == userId).ToList();
            return transactions.Select(userTransaction => toTransactionResponse(userTransaction)).ToArray();
        }

        private async Task<TransactionResponse[]?> GetServiceProviderTransactionsAsync(long id)
        {
            var transactions = await _transactionRepository.GetAllAsync("Gig", "Rating", "Item");
            if (transactions == null) return null;
            transactions = transactions.Where(tr => tr.Gig.UserId == id).ToList();
            return transactions.Select(t => toTransactionResponse(t)).ToArray();
        }


        private TransactionResponse toTransactionResponse(Transaction userTransaction)
        {
            return new TransactionResponse
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
            };
        }

    }
}
