using bsc_be.DTOs;
using bsc_be.Models;

namespace bsc_be.Services
{
    public interface IGigService
    {
        public Task<List<GigResponse>> GetGigsAsync(GigQueryParams queryParam);
        public Task<GigDetailResponse?> GetGigByIdAsync(long id);
    }
}