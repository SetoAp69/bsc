using bsc_be.DTOs;
using bsc_be.Exceptions;
using bsc_be.Models;
using bsc_be.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bsc_be.Controllers
{
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IRatingService _ratingService;
        public TransactionController(ITransactionService transactionService, IRatingService ratingService)
        {
            _transactionService = transactionService;
            _ratingService = ratingService;
        }
        [Authorize]
        [HttpGet("api/transactions")]
        public async Task<IActionResult> GetTransactions()
        {
            var userId = long.Parse(User.FindFirst("userId")!.Value);
            var roleString = User.FindFirst("userRole")!.Value;
            Enum.TryParse(roleString, out UserRole role);
            var transactions = await _transactionService.GetTransactionsAsync(userId, role);
            if (transactions == null)
            {
                return NoContent();
            }
            return Ok(transactions);
        }

        [Authorize]
        [HttpGet("api/transactions/{transactionId}")]
        public async Task<IActionResult> GetTransactionById(int transactionId)
        {
            try
            {
                var transaction = await _transactionService.GetTransactionByIdAsync(transactionId);
                return Ok(transaction);
            }
            catch (TransactionNotFoundException ex)
            {
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Authorize]
        [HttpPost("api/transactions")]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionRequest request)
        {
            var userId = long.Parse(User.FindFirst("userId")!.Value);
            try
            {
                var transaction = await _transactionService.CreateTransactionAsync(userId, request);
                if (transaction)
                {
                    return Created();
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
        [HttpPut("api/transactions/item")]
        public async Task<IActionResult> UpdateTransactionItem([FromBody] TransactionItemUpdateRequest request)
        {
            try
            {
                var isSuccess = await _transactionService.UpdateTransactionItemAsync(request);
                return Ok(new { status = "Success", message = "Transaction status updated successfully" });
            }
            catch (TransactionFinishedException e)
            {
                return Conflict(new { status = "Error", message = e.Message });
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
                await _transactionService.DeleteTransactionAsync(transactionId);
                return Ok(new { status = "Success", message = "Transaction deleted successfully" });
            }
            catch (TransactionNotFoundException)
            {
                return NoContent();
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

    }
}
