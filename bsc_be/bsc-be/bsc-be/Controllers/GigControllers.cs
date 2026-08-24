using bsc_be.DTOs;
using bsc_be.Models;
using bsc_be.Services;
using Microsoft.AspNetCore.Authorization;
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
            if (gig == null) return NoContent();

            return Ok(gig);
        }
        [HttpGet("{id:long}/ratings")]
        public async Task<IActionResult> GetGigRatings(long id)
        {
            var ratings = await _gigService.GetGigRatingAsync(id);
            if (ratings == null) return NoContent();
            return Ok(ratings);
        }
        [Authorize]
        [HttpPost()]
        public async Task<IActionResult> CreateGig([FromBody] GigRequest request)
        {
            var userId = long.Parse(User.FindFirst("userId")!.Value);
            var roleString = User.FindFirst("userRole")!.Value;
            Enum.TryParse(roleString, out UserRole role);
            if (role != UserRole.SERVICE_PROVIDER)
            {
                return BadRequest("User with this role can't craete a gig");
            }
            try
            {
                var gig = await _gigService.CreateGigAsync(userId, request);
                if (gig != null)
                {
                    return Ok(new { status = "Success", message = "Gig created successfully", transaction = gig.Id });
                }
                else
                {
                    return BadRequest(new { status = "Error", message = "Creation failed" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "Error", message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateGig(long id, [FromBody] GigEditRequest request)
        {
            var userId = long.Parse(User.FindFirst("userId")!.Value);
            var roleString = User.FindFirst("userRole")!.Value;
            Enum.TryParse(roleString, out UserRole role);
            if (role != UserRole.SERVICE_PROVIDER)
            {
                return BadRequest("User with this role can't update a gig");
            }
            try
            {
                var gig = await _gigService.UpdateGigAsync(id, userId, request);
                if (gig != null)
                {
                    return Ok(new { status = "Success", message = "Gig updated successfully", transaction = gig.Id });
                }
                else
                {
                    return BadRequest(new { status = "Error", message = "Update failed" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "Error", message = ex.Message });
            }
        }
    }
}