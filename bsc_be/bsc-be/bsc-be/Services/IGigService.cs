using bsc_be.DTOs;

namespace bsc_be.Services
{
    public interface IGigService
    {
        public Task<List<GigResponse>> GetGigsAsync(GigQueryParams queryParam);
        public Task<GigDetailResponse?> GetGigByIdAsync(long id);
    }
}