using bsc_be.DTOs;
using bsc_be.Services;
using Microsoft.AspNetCore.Mvc;

namespace bsc_be.Controllers
{
    [ApiController]
    [Route("api/gigs")]
    public class GigControllers : ControllerBase
    {
        private readonly IGigService _gigService;

        public GigControllers(IGigService gigService)
        {
            _gigService = gigService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetGigs([FromQuery] GigQueryParams queryParams)
        {
            var gigs = await _gigService.GetGigsAsync(queryParams);
            return Ok(gigs);
        }
        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetGigById(long id)
        {
            var gig = await _gigService.GetGigByIdAsync(id);
            if (gig == null) return NotFound();
            return Ok(gig);
        }
    }
}