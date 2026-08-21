using bsc_be.DTOs;
using bsc_be.Models;
using bsc_be.Services;
using Microsoft.AspNetCore.Mvc;

namespace bsc_be.Controllers
{
    public class ItemController: ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpPut("api/item")]
        public async Task<IActionResult> updateItem(ItemRequest request)
        {
            try
            {
                var newItem = await _itemService.UpdateItemAsync(request);
                return Ok(new { status = "Success", message = "Transaction rating updated successfully", newItem = newItem });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "Error", message = ex.Message });
            }
        }
    }
}
