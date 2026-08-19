using bsc_be.DTOs;

namespace bsc_be.Services
{
    public interface IRatingService
    {
        Task<RatingResponse?> UpdateRatingAsync(TransactionRatingUpdateRequest request);
    }
}
