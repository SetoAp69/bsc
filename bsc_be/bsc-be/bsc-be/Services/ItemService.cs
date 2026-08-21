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
        public async Task<ItemResponse?> CreateItemAsync(ItemRequest request)
        {
            var item = new Item
            {
                Name = request.Name,
                Description = request.Description,
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

        public async Task<ItemResponse> UpdateItemAsync(ItemRequest request)
        {
            var item = await _itemRepository.GetByIdAsync(request.Id);
            if(item == null)
            {
                throw new Exception("Item not found");
            }
            item.Name = request.Name;
            item.Description = request.Description;
            item.Path = request.Path;
            await _itemRepository.SaveChangesAsync();
            return new ItemResponse
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Path = item.Path
            };
        }
    }
}
