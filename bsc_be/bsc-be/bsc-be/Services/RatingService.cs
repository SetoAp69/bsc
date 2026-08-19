using bsc_be.DTOs;
using bsc_be.Models;
using bsc_be.Repositories;

namespace bsc_be.Services
{
    public class RatingService: IRatingService
    {
        private readonly IRepository<Rating> _ratingRepository;
        public RatingService(IRepository<Rating> ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public async Task<RatingResponse?> UpdateRatingAsync(TransactionRatingUpdateRequest request)
        {
            var rating = await _ratingRepository.GetByIdAsync(request.RatingId);
            if (rating == null)
            {
                return null;
            }
            rating.Star = request.StarRating;
            rating.Comment = request.Comment;
            await _ratingRepository.SaveChangesAsync();
            return new RatingResponse
            {
                Id = rating.Id,
                Rating = rating.Star,
                Comment = rating.Comment
            };
        }
    }
}
