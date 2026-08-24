using bsc_be.DTOs;
using bsc_be.Exceptions;
using bsc_be.Models;
using bsc_be.Repositories;

namespace bsc_be.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<Transaction> _transactionRepository;
        private readonly IRepository<Gig> _gigRepository;
        private readonly IConfiguration _configuration;
        private readonly IRepository<Item> _itemRepository;
        private readonly ILogger<TransactionService> _logger;
        public TransactionService(
            IRepository<Transaction> transactionRepository,
            IRepository<Gig> gigRepository,
            IConfiguration configuration,
            IRepository<Item> itemRepository,
            ILogger<TransactionService> logger
            )
        {
            _transactionRepository = transactionRepository;
            _gigRepository = gigRepository;
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
                throw new TransactionNotFoundException(transactionId.ToString());
            }
            return toTransactionResponse(transaction);
        }

        public async Task<Boolean> CreateTransactionAsync(long UserId, TransactionRequest request)
        {
            var gig = await _gigRepository.GetByIdAsync(request.GigId);
            if(gig==null){
                throw new GigNotFoundException(request.GigId);
            }
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
                TotalPriceReceived = gig.Price,
                BasePrice = gig.Price,
                TotalPricePaid = request.TotalPrice,
                PaymentMethodId = request.PaymentMethodId,
                Deadline = DateTime.UtcNow.AddDays(gig?.Duration ?? 0),
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
            var transaction = await _transactionRepository.GetByIdAsync(request.Id);
            if (transaction == null)
            {
                _logger.LogWarning("Transaction not found for transaction id {TransactionId}", request.Id);
                throw new TransactionNotFoundException(request.Id.ToString());
            }
            if (transaction.Status == Status.FINISHED || transaction.Status == Status.CANCELED)
            {
                throw new TransactionFinishedException(request.Id.ToString(),transaction.Status.ToString());
            }
            var itemId = transaction.ItemId;
            await _transactionRepository.BeginTransactionAsync();

            try
            {
                var requestStatus = Enum.Parse<Status>(request.TransactionStatus);
                transaction.Status = requestStatus;
                var item = await _itemRepository.GetByIdAsync(itemId.Value);
                item.Name = request.Item.Name;
                item.Description = request.Item.Description;
                item.Path = request.Item.Path;

                if (requestStatus == Status.FINISHED)
                {
                    transaction.TotalPriceReceived = calculateFinalPrice(transaction);
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
            if(transaction == null) return 0m;
            var deadline = transaction.Deadline;
            var overdue = (DateTime.Now - deadline).Days;

            if (overdue <= 0)
            {
                return transaction.BasePrice;
            }
            var penaltyRate = 0.01m;
            var basePenaltyRate = 0.1m; 
            var basePenalty = transaction.BasePrice * basePenaltyRate; 
            var maxPenaltyPercentage = 0.5m;
            var maxPenalty = transaction.BasePrice * maxPenaltyPercentage;
            var penalty = basePenalty * (decimal)Math.Pow((double)(1 + penaltyRate), overdue);
            if (penalty > maxPenalty)
            {
                penalty = maxPenalty;
            }
            return Math.Max(0, transaction.BasePrice - penalty);
        }


        public async Task DeleteTransactionAsync(long transactionId)
        {
            var transaction = await _transactionRepository.GetByIdAsync(transactionId);
            if (transaction == null)
            {
                _logger.LogWarning("Transaction not found for transaction id {TransactionId}", transaction);
                throw new TransactionNotFoundException(transactionId.ToString());
            }

            await _transactionRepository.BeginTransactionAsync();
            try
            {
                transaction.Status = Status.CANCELED;
                _transactionRepository.Update(transaction);
                await _transactionRepository.SaveChangesAsync();
                await _transactionRepository.CommitTransactionAsync();
            }
            catch
            {
                await _transactionRepository.RollbackTransactionAsync();
                throw new Exception();
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
                BasePrice = userTransaction.BasePrice,
                TotalPriceReceived = userTransaction.TotalPriceReceived,
                TotalPricePaid = userTransaction.TotalPricePaid,
                BuyerDescription = userTransaction.BuyerDescription,
                Deadline = userTransaction.Deadline,
                Rating = new RatingResponse
                {
                    Id = userTransaction.Rating?.Id??0,
                    Rating = userTransaction.Rating?.Star??0,
                    Comment = userTransaction.Rating?.Comment??""
                },
                Item = new ItemResponse
                {
                    Id = userTransaction.Item?.Id??0,
                    Name = userTransaction.Item?.Name??"",
                    Path = userTransaction.Item?.Path??""
                }
            };
        }

    }
}
