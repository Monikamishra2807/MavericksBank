using MavericksBank.DTOs;
using MavericksBank.Models;
using MavericksBank.Repositories.Interfaces;
using MavericksBank.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MavericksBank.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            IAuthRepository repository,
            IConfiguration configuration,
            ILogger<AuthService> logger)
        {
            _repository = repository;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<string> RegisterAsync(RegisterDto registerDto)
        {
            _logger.LogInformation("Registration request received for Email: {Email}", registerDto.Email);

            var existingUser = await _repository.GetUserByEmailAsync(registerDto.Email);

            if (existingUser != null)
            {
                _logger.LogWarning("Registration failed. Email already exists: {Email}", registerDto.Email);
                throw new Exception("Email already in use.");
            }

            var user = new User
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                Mobile = registerDto.Mobile,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                Role = registerDto.Role.Trim().ToLower() switch
                {
                    "customer" => "Customer",
                    "admin" => "Admin",
                    _ => throw new Exception("Role must be either Customer or Admin.")
                }
            };

            await _repository.AddUserAsync(user);
            await _repository.SaveChangesAsync();

            _logger.LogInformation(
                "User registered successfully. Email: {Email}, Role: {Role}",
                user.Email,
                user.Role);

            return "User registered successfully.";
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginDto loginDto)
        {
            _logger.LogInformation("Login attempt for Email: {Email}", loginDto.Email);

            var user = await _repository.GetUserByEmailAsync(loginDto.Email);

            if (user == null)
            {
                _logger.LogWarning("Login failed. User not found. Email: {Email}", loginDto.Email);
                return null;
            }

            var isValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);

            if (!isValid)
            {
                _logger.LogWarning("Login failed. Invalid password for Email: {Email}", loginDto.Email);
                return null;
            }

            _logger.LogInformation(
                "User logged in successfully. Email: {Email}, Role: {Role}",
                user.Email,
                user.Role);

            return new LoginResponseDto
            {
                Token = GenerateToken(user),
                Role = user.Role,
                FullName = user.FullName
            };
        }

        public string GenerateToken(User user)
        {
            var jwtSection = _configuration.GetSection("JwtSettings");

            var secret = jwtSection.GetValue<string>("Secret")
                ?? throw new InvalidOperationException("JWT Secret not configured");

            var issuer = jwtSection.GetValue<string>("Issuer");
            var audience = jwtSection.GetValue<string>("Audience");
            var expiryMinutes = jwtSection.GetValue<int?>("ExpiryMinutes") ?? 60;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("fullname", user.FullName),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}