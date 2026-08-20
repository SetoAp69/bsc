using bsc_be.DTOs;

namespace bsc_be.Services
{
    public interface IItemService
    {
        Task<ItemResponse?> CreateItemAsync(ItemRequest request);
    }
}
