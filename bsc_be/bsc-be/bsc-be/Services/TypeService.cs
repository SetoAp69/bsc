using bsc_be.Repositories;
using bsc_be.Models;

namespace bsc_be.Services
{
    public class TypeService : ITypeService
    {
        private readonly IRepository<Models.Type> _typeRepository;
        public TypeService(
            IRepository<Models.Type> typeRepository
        )
        {
            _typeRepository = typeRepository;
        }

        public async Task<List<Models.Type>> GetTypesAsync()
        {
            var types =  await _typeRepository.GetAllAsync();
            return types.ToList();
        }
    }
}