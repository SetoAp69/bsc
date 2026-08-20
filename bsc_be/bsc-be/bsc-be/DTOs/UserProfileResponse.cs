namespace bsc_be
{
    public class UserProfileResponse()
    {
        public long Id { get; set; } = 0;
        public string Username { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? About { get; set; } = null;
        public string? Location { get; set; } = null;
        public string UserRole { get; set; } = string.Empty;
        public UserRating? Rating { get; set; } = null;

        public class UserRating
        {
            public decimal Stars { get; set; } = 0m;
            public int Count { get; set; } = 0;
        }
    }
}