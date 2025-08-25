namespace IntegracaoWebApi.Application.DTOs.Auth
{
    public class RegisterRequest
    {
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
