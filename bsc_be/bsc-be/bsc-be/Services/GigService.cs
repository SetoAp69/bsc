
using bsc_be.DTOs;
using bsc_be.Models;
using bsc_be.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace bsc_be.Services
{
    class GigService : IGigService
    {
        private readonly IRepository<Gig> _gigRepository;
        private readonly IRepository<GigType> _gigTypeRepository;
        private readonly IRepository<bsc_be.Models.Type> _typeRepository;

        public GigService(
            IRepository<Gig> gigRepository,
            IRepository<GigType> gigTypeRepository,
            IRepository<bsc_be.Models.Type> typeRepository
        )
        {
            _gigRepository = gigRepository;
            _typeRepository = typeRepository;
            _gigTypeRepository = gigTypeRepository;
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
                var typesFilter = queryParams.Types.ToHashSet();
                var userId = queryParams.UserId;
                if (!searchQuery.IsNullOrEmpty())
                {
                    gigs = gigs
                        .Where(g => g.Name.Contains(searchQuery!, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }
                if (typesFilter.Count() > 0)
                {
                    gigs = gigs
                        .Where(g =>
                            g.GigTypes.Any(gt => typesFilter.Contains(gt.Type.Name))
                        )
                        .ToList();
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

        public async Task<List<GigRatingResponse>?> GetGigRatingAsync(long id)
        {
            var gig = await _gigRepository.GetByIdAsync(id, "Transactions.Rating", "Transactions.User");
            if (gig == null) return null;
            var ratings = gig
            .Transactions
            .Where(t => t.Rating?.Star > 0)
            .Select(t => toGigRatingResponse(t))
            .ToList();
            return ratings;
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
            return gig
                .Transactions
                .Where(t => t.Rating?.Star > 0)
                .Average(t => t.Rating?.Star) ?? 0;
        }

        private GigRatingResponse toGigRatingResponse(Transaction transaction)
        {
            return new GigRatingResponse
            {
                Id = transaction.Rating!.Id,
                userName = transaction.User!.Username,
                Rating = transaction.Rating!.Star,
                Comment = transaction.Rating!.Comment,
            };
        }

        public async Task<Gig?> CreateGigAsync(long UserId, GigRequest Request)
        {
            await _gigTypeRepository.BeginTransactionAsync();
            try
            {
                var types = await _typeRepository.GetAllAsync();
                types = types
                .Where(t => Request.Types.Contains(t.Id))
                .ToList();

                var gig = new Gig
                {
                    Name = Request.Name,
                    Description = Request.Description,
                    UserId = UserId,
                    Duration = Request.Duration,
                    Price = Request.Price,
                };

                await _gigRepository.AddAsyncThenGet(gig);
                types.ForEach(t =>
                    _gigTypeRepository.AddAsync(
                        new GigType
                        {
                            GigId = gig.Id,
                            TypeId = t.Id
                        }
                    )
                );

                await _gigTypeRepository.SaveChangesAsync();
                var created = await _gigRepository.GetByIdAsync(gig.Id);
                await _gigTypeRepository.CommitTransactionAsync();
                return created;
            }
            catch (Exception e)
            {
                await _gigTypeRepository.RollbackTransactionAsync();
                return null;
            }
        }
    }
}