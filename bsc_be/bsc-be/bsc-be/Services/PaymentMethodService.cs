using bsc_be.Models;
using bsc_be.Repositories;

namespace bsc_be.Services
{
    class PaymentMethodService : IPaymentMethodService
    {
        private readonly IRepository<PaymentMethod> _paymentMethodRepository;
        public PaymentMethodService(
            IRepository<PaymentMethod> paymentRepository
        )
        {
            _paymentMethodRepository = paymentRepository;
        }

        public async Task<List<PaymentMethod>> GetPaymentMethodsAsync()
        {
            try
            {
                var paymentMethods = await _paymentMethodRepository.GetAllAsync();
                return paymentMethods;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}