using bsc_be.Services;
using Microsoft.AspNetCore.Mvc;

namespace bsc_be.Controllers
{
    [ApiController]
    [Route("api/types")]
    public class TypeController : ControllerBase
    {
        private readonly ITypeService _typeService;

        public TypeController(ITypeService typeService)
        {
            _typeService = typeService;
        }
        [HttpGet()]
        public async Task<IActionResult> GetController()
        {
            var types = await _typeService.GetTypesAsync();
            return Ok(types);
        }
    }
}