
using bsc_be.Models;
using bsc_be.Repositories;

namespace bsc_be.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserProfileResponse?> GetUserProfileAsync(long id)
        {
            var user = await _userRepository.GetByIdAsync(
                id, "Gigs.Transactions", "Gigs.Transactions.Rating");
            if (user == null) return null;
            return toUserProfileResponse(user);
        }

        private UserProfileResponse toUserProfileResponse(User user)
        {
            var userProfile = new UserProfileResponse
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                UserRole = user.UserRole.ToString(),
                Email = user.Email,
                About = user.About,
                Location = user.Location,
            };

            if (user.UserRole != UserRole.SERVICE_PROVIDER) return userProfile;
            userProfile.Rating = calculateRating(user);
            return userProfile;
        }

        private UserProfileResponse.UserRating calculateRating(User user)
        {
            var ratings = user
                .Gigs
                .SelectMany(g => g.Transactions)
                .Where(t => t.Rating != null)
                .Select(t => t.Rating);

            var stars = ratings.Average(r => r!.Star);
            var count = ratings.Count();

            return new UserProfileResponse.UserRating
            {
                Stars = stars,
                Count = count
            };
        }
    }
}