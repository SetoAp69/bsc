using bsc_be.DTOs;
using bsc_be.Models;
using bsc_be.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace bsc_be.Services
{
    class GigService : IGigService
    {
        private readonly Repository<Gig> _gigRepository;
        private readonly Repository<User> _userRepository;
        private readonly Repository<Transaction> _transactionRepository;
        private readonly Repository<Rating> _ratingRepository;

        public GigService(
            Repository<Gig> gigRepository,
            Repository<User> userRepository,
            Repository<Transaction> transactionRepository,
            Repository<Rating> ratingRepository
        )
        {
            _gigRepository = gigRepository;
            _userRepository = userRepository;
            _transactionRepository = transactionRepository;
            _ratingRepository = ratingRepository;
        }

        public async Task<GigDetailResponse?> GetGigByIdAsync(long id)
        {
            try
            {
                var gig = await _gigRepository.GetByIdAsync(
                  id, "Transaction.Rating", "GigTypes.Type"
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
                var gigs = await _gigRepository.GetAllAsync();
                var searchQuery = queryParams.Search;
                var typesFilter = queryParams.Types;
                if (!searchQuery.IsNullOrEmpty())
                {
                    gigs = gigs
                        .Where(g => g.Name.Contains(searchQuery!, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }
                //TODO Add filter by list type
                // if (typesFilter.Count() > 0)
                // // {
                // //     gigs = gigs
                // //         .Where(g=> g.GigTypes.Select(gt=>gt.Type))
                // }
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

            decimal totalStarRating = transactionsWithRating
                    .Sum(t => t.Rating!.Star);

            return totalStarRating / transactionsWithRating.Count();
        }

    }
}