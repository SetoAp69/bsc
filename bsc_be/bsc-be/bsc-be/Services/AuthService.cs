using bsc_be.DTOs;
using bsc_be.Models;
using bsc_be.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace bsc_be.Services
{
    public class AuthService: IAuthService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IConfiguration _configuration;
        
        public AuthService(IRepository<User> userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }
        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            List<User> userList = await _userRepository.GetAllAsync();
            User? user = userList.FirstOrDefault(u => u.Username == request.Username && u.Password == request.Password);
            if (user == null)
            {
                return null;
            }

            Claim[] claims = new[]
            {
                new Claim("userId", user.Id.ToString()),
                new Claim("email", user.Email)
            };
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(_configuration["Jwt:ExpireMinutes"]!)),
                signingCredentials: creds
            );

            return new LoginResponse
            {
                Jwt = new JwtSecurityTokenHandler().WriteToken(token),
                Status = "Success",
            };
        }
    }
}
