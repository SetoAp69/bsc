namespace bsc_be.DTOs
{
    public class LoginResponse
    {
        public string Jwt { get; set; } = string.Empty;
        public string Status { get; set;} = string.Empty;
    }
}