using bsc_be.Models;

namespace bsc_be.Services
{
    public interface IPaymentMethodService
    {
        public Task<List<PaymentMethod>> GetPaymentMethodsAsync();
    }
}