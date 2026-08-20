namespace bsc_be
{
    public interface IUserService
    {
        Task<UserProfileResponse?> GetUserProfileAsync(long id);
        
    }
}