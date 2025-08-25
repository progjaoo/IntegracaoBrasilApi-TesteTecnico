using IntegracaoWebApi.Application.DTOs.Auth;

namespace IntegracaoWebApi.Infrastructure.Auth
{
    public interface IAuthService
    {
        Task<AuthResponse?> LoginAsync(LoginRequest request);
        Task<AuthResponse?> RegisterAsync(RegisterRequest request);
    }
}
