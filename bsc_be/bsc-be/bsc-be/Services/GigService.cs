using bsc_be.DTOs;
using bsc_be.Models;
using bsc_be.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace bsc_be.Services
{
    class GigService : IGigService
    {
        private readonly IRepository<Gig> _gigRepository;

        public GigService(
            IRepository<Gig> gigRepository
        )
        {
            _gigRepository = gigRepository;
        }

        public async Task<GigDetailResponse?> GetGigByIdAsync(long id)
        {
            try
            {
                var gig = await _gigRepository.GetByIdAsync(
                  id, "Transactions.Rating", "GigTypes.Type", "User.Gigs"
                );
                return gig != null ? toGigDetailResponse(gig) : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<GigResponse>> GetGigsAsync(GigQueryParams queryParams)
        {
            try
            {
                var gigs = await _gigRepository.GetAllAsync("Transactions.Rating", "GigTypes.Type", "User.Gigs");
                var searchQuery = queryParams.Search;
                var typesFilter = queryParams.Types;
                var userId = queryParams.UserId;
                if (!searchQuery.IsNullOrEmpty())
                {
                    gigs = gigs
                        .Where(g => g.Name.Contains(searchQuery!, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }
                // TODO: refactor this
                if (typesFilter.Count() > 0)
                {
                    gigs = gigs
                        .Where(g => !typesFilter.Intersect(g.GigTypes.Select(gt => gt.Type.Name).ToList()).IsNullOrEmpty()).ToList();
                }
                if (userId != null)
                {
                    gigs = gigs
                        .Where(g => g.UserId == userId)
                        .ToList();
                }
                gigs.Skip((queryParams.Page - 1) * queryParams.Limit)
                    .Take(queryParams.Limit)
                    .ToList();
                return gigs.Select(g => toGigResponse(g)).ToList();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public GigDetailResponse toGigDetailResponse(Gig gig)
        {
            return new GigDetailResponse
            {
                Id = gig.Id,
                Name = gig.Name,
                Description = gig.Description,
                Duration = gig.Duration,
                Price = gig.Price,
                Stars = calculateStars(gig),
                GigCreator = new GigDetailResponse.Creator
                {
                    Id = gig.User.Id,
                    Name = gig.User.Name
                },
                Types = gig.GigTypes.Select(gt =>
                {
                    return new GigDetailResponse.Type
                    {
                        Name = gt.Type.Name,
                        Id = gt.TypeId
                    };
                }
                ).ToList()
            };
        }

        private GigResponse toGigResponse(Gig gig)
        {
            return new GigResponse
            {
                Id = gig.Id,
                Name = gig.Name,
                Price = gig.Price,
                Stars = calculateStars(gig),
                GigCreator = gig.User.Name,
                Types = gig.GigTypes.Select(gt => gt.Type.Name).ToList()
            };
        }

        private decimal calculateStars(Gig gig)
        {
            var transactionsWithRating = gig
                .Transactions
                .Where(t => t.Rating != null);
            var count = transactionsWithRating.Count();
            if (count <= 0) return 0;
            decimal totalStarRating = transactionsWithRating
                    .Sum(t => t.Rating!.Star);

            return totalStarRating / count;
        }

    }
}