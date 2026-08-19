using bsc_be.Services;
using Microsoft.AspNetCore.Mvc;

namespace bsc_be.Controllers
{
    
    [ApiController]
    [Route("api/payment-methods")]
    public class PaymentMethodController : ControllerBase
    {
        private readonly IPaymentMethodService _paymentMethodService;
        public PaymentMethodController(IPaymentMethodService paymentMethodService)
        {
            _paymentMethodService = paymentMethodService;
        }
        [HttpGet()]
        public async Task<IActionResult> GetPaymentMethods()
        {
            var paymentMethods =  await _paymentMethodService.GetPaymentMethodsAsync();
            return Ok(paymentMethods);
        }
    }
}