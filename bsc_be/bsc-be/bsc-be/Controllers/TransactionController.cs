using bsc_be.DTOs;
using bsc_be.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bsc_be.Controllers
{
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IRatingService _ratingService;
        private readonly IItemService _itemService;
        public TransactionController(ITransactionService transactionService, IRatingService ratingService, IItemService itemService)
        {
            _transactionService = transactionService;
            _ratingService = ratingService;
            _itemService = itemService;
        }
        [Authorize]
        [HttpGet("api/transactions/{userId}")]
        public async Task<IActionResult> GetTransactions(int userId)
        {
            var transactions = await _transactionService.GetTransactionAsync(userId);
            if (transactions == null)
            {
                return NotFound();
            }
            return Ok(transactions);
        }
        [Authorize]
        [HttpPost("api/transactions")]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionRequest request)
        {
            try
            {
                var newItem = await CreateItem(request.ItemName);
                if (newItem == null) return BadRequest(new { status = "Error", message = "Creation failed" });
                var transaction = await _transactionService.CreateTransactionAsync(request, newItem.Id);
                if (transaction)
                {
                    return Ok(new { status = "Success", message = "Transaction created successfully", transaction = transaction });
                }
                else
                {
                    return BadRequest(new { status = "Error", message = "Creation failed" });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "Error", message = ex.Message });
            }
        }
        [Authorize]
        [HttpPut("api/transactions/status")]
        public async Task<IActionResult> UpdateTransactionStatus([FromBody] TransactionStatusRequest request)
        {
            try
            {
                var transaction = await _transactionService.UpdateTransactionStatusAsync(request);
                return Ok(new { status = "Success", message = "Transaction status updated successfully", transaction = transaction });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "Error", message = ex.Message });
            }
        }
        [Authorize]
        [HttpDelete("api/transactions/{transactionId}")]
        public async Task<IActionResult> DeleteTransaction(int transactionId)
        {
            try
            {
                var isDeleteSuccess = await _transactionService.DeleteTransactionAsync(transactionId);
                if (isDeleteSuccess)
                    return Ok(new { status = "Success", message = "Transaction deleted successfully" });
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "Error", message = ex.Message });
            }
        }
        [Authorize]
        [HttpPut("api/transactions/rating")]
        public async Task<IActionResult> UpdateTransactionRating([FromBody] TransactionRatingUpdateRequest request)
        {
            try
            {
                var rating = await _ratingService.UpdateRatingAsync(request);
                return Ok(new { status = "Success", message = "Transaction rating updated successfully", rating = rating });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "Error", message = ex.Message });
            }
        }

        private async Task<ItemResponse?> CreateItem(string itemName)
        {
            return await _itemService.CreateItemAsync(itemName);
        }
    }
}
