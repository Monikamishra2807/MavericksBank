using MavericksBank.DTOs;
using MavericksBank.Models;
using MavericksBank.Repositories.Interfaces;
using MavericksBank.Services.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;

namespace MavericksBank.Test.Services
{
    [TestFixture]
    public class AuthServiceTests
    {
        private Mock<IAuthRepository> _repository;
        private IConfiguration _configuration;
        private AuthService _service;

        [SetUp]
        public void SetUp()
        {
            _repository = new Mock<IAuthRepository>();

            var settings = new Dictionary<string, string?>
            {
                { "JwtSettings:Secret", "ThisIsMyVerySecretKeyForJwtAuthentication12345" },
                { "JwtSettings:Issuer", "MavericksBank" },
                { "JwtSettings:Audience", "MavericksBankUsers" },
                { "JwtSettings:ExpiryMinutes", "60" }
            };

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(settings)
                .Build();

            _service = new AuthService(
                _repository.Object,
                _configuration,
                 NullLogger<AuthService>.Instance);
        }

        [Test]
        public async Task When_Register_NewUser_ReturnsSuccessMessage()
        {
            // Arrange
            var dto = new RegisterDto
            {
                FullName = "Monika",
                Email = "monika@gmail.com",
                Mobile = "9876543210",
                Password = "Password123",
                Role = "Customer"
            };

            _repository.Setup(x => x.GetUserByEmailAsync(dto.Email))
                       .ReturnsAsync((User?)null);

            // Act
            var result = await _service.RegisterAsync(dto);

            // Assert
            Assert.That(result, Is.EqualTo("User registered successfully."));

            _repository.Verify(x => x.AddUserAsync(It.IsAny<User>()), Times.Once);
            _repository.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public void When_Register_DuplicateEmail_ThrowsException()
        {
            // Arrange
            var dto = new RegisterDto
            {
                FullName = "Monika",
                Email = "monika@gmail.com",
                Mobile = "9876543210",
                Password = "Password123",
                Role = "Customer"
            };

            _repository.Setup(x => x.GetUserByEmailAsync(dto.Email))
                       .ReturnsAsync(new User());

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () =>
                await _service.RegisterAsync(dto));

            Assert.That(ex.Message, Is.EqualTo("Email already in use."));
        }

        [Test]
        public async Task When_Login_ValidCredentials_ReturnsToken()
        {
            // Arrange
            var password = BCrypt.Net.BCrypt.HashPassword("Password123");

            var user = new User
            {
                UserId = 1,
                FullName = "Monika",
                Email = "monika@gmail.com",
                Password = password,
                Role = "Customer"
            };

            _repository.Setup(x => x.GetUserByEmailAsync(user.Email))
                       .ReturnsAsync(user);

            var dto = new LoginDto
            {
                Email = "monika@gmail.com",
                Password = "Password123"
            };

            // Act
            var token = await _service.LoginAsync(dto);

            // Assert
            Assert.That(token, Is.Not.Null);
        }

        [Test]
        public async Task When_Login_InvalidPassword_ReturnsNull()
        {
            // Arrange
            var password = BCrypt.Net.BCrypt.HashPassword("Password123");

            var user = new User
            {
                UserId = 1,
                FullName = "Monika",
                Email = "monika@gmail.com",
                Password = password,
                Role = "Customer"
            };

            _repository.Setup(x => x.GetUserByEmailAsync(user.Email))
                       .ReturnsAsync(user);

            var dto = new LoginDto
            {
                Email = "monika@gmail.com",
                Password = "WrongPassword"
            };

            // Act
            var result = await _service.LoginAsync(dto);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task When_Login_InvalidEmail_ReturnsNull()
        {
            // Arrange
            _repository.Setup(x => x.GetUserByEmailAsync(It.IsAny<string>()))
                       .ReturnsAsync((User?)null);

            var dto = new LoginDto
            {
                Email = "wrong@gmail.com",
                Password = "Password123"
            };

            // Act
            var result = await _service.LoginAsync(dto);

            // Assert
            Assert.That(result, Is.Null);
        }
    }
}