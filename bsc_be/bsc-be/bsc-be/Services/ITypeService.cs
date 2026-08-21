using bsc_be.Models;
namespace bsc_be.Services
{
    public interface ITypeService
    {
        public Task<List<Models.Type>> GetTypesAsync();
    }
}