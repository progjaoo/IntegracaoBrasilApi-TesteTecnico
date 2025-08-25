namespace IntegracaoWebApi.Application.DTOs.Auth
{
    public class AuthResponse
    {
        public string Token { get; set; } = default!;
        public DateTime ExpiresAtUtc { get; set; }
        public string Username { get; set; } = default!;
        public string Role { get; set; } = default!;
    }
}
