using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagementApi.Interfaces;
using TaskManagementApi.Models;

namespace TaskManagementApi.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository<UserResponseDto> _userRepository;
        private readonly IConfiguration _configuration;

        public AuthenticationService(
            IUserRepository<UserResponseDto> userRepository,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public AuthResponseDto? Authenticate(string username, string password)
        {
            var user = _userRepository.ValidateCredentials(username, password);
            if (user == null) return null;

            var token = GenerateToken(user);
            return new AuthResponseDto
            {
                User = user,
                Token = token
            };
        }

        public string GenerateToken(UserResponseDto user)
        {
            var secret = _configuration["Jwt:Secret"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret ?? throw new InvalidOperationException("JWT Secret not configured")));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
