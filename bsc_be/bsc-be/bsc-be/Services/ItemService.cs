using bsc_be.DTOs;
using bsc_be.Models;
using bsc_be.Repositories;

namespace bsc_be.Services
{
    public class ItemService: IItemService
    {
        private readonly IRepository<Item> _itemRepository;

        public ItemService(IRepository<Item> itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public async Task<ItemResponse?> CreateItemAsync(string itemName)
        {
            var item = new Item
            {
                Name = itemName,
                Path = string.Empty
            };
            try
            {
                var newItem = await _itemRepository.AddAsyncThenGet(item);
                return new ItemResponse{
                    Id = newItem.Id,
                    Name = newItem.Name,
                    Path = newItem.Path
                };
            }
            catch
            {
                throw;
            }
        }
    }
}
