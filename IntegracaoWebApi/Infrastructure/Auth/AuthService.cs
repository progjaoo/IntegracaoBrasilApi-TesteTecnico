using IntegracaoWebApi.Application.DTOs.Auth;
using IntegracaoWebApi.Application.Services;
using IntegracaoWebApi.Core.Entities;
using IntegracaoWebApi.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace IntegracaoWebApi.Infrastructure.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly TokenService _tokenSvc;
        private readonly ILogger<AuthService> _logger;
        private readonly PasswordHasher<User> _hasher = new();

        public AuthService(IUserRepository userRepo, TokenService tokenSvc, ILogger<AuthService> logger)
        {
            _userRepo = userRepo;
            _tokenSvc = tokenSvc;
            _logger = logger;
        }
        public async Task<AuthResponse?> LoginAsync(LoginRequest request)
        {
            var user = await _userRepo.GetByUsernameAsync(request.Username);
            if (user is null) return null;

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (result == PasswordVerificationResult.Failed) return null;

            var (token, exp) = _tokenSvc.Generate(user);
            return new AuthResponse
            {
                Token = token,
                ExpiresAtUtc = exp,
                Username = user.Username,
                Role = user.Role
            };
        }
        public async Task<AuthResponse?> RegisterAsync(RegisterRequest request)
        {
            var exists = await _userRepo.GetByUsernameAsync(request.Username);
            if (exists is not null) return null; 

            var user = new User { Username = request.Username, Role = "User" };
            user.PasswordHash = _hasher.HashPassword(user, request.Password);

            await _userRepo.AddAsync(user);

            var (token, exp) = _tokenSvc.Generate(user);
            return new AuthResponse
            {
                Token = token,
                ExpiresAtUtc = exp,
                Username = user.Username,
                Role = "User"
            };
        }
    }
}
