using bsc_be.DTOs;
using bsc_be.Services;
using Microsoft.AspNetCore.Mvc;

namespace bsc_be.Controllers
{
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
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
        [HttpPost("api/transactions")]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionRequest request)
        {
            try
            {
                var transaction = await _transactionService.CreateTransactionAsync(request);
                return Ok(new { status = "Success", message = "Transaction created successfully", transaction = transaction});

            } catch (Exception ex)
            {
                return BadRequest(new { status = "Error", message = ex.Message });
            }
        }
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
    }
}
