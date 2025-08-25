using IntegracaoWebApi.Application.DTOs.Auth;
using IntegracaoWebApi.Infrastructure.Auth;
using Microsoft.AspNetCore.Mvc;

namespace IntegracaoWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        public AuthController(IAuthService auth) => _auth = auth;

        /// <summary>Autentica e retorna o JWT.</summary>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            var result = await _auth.LoginAsync(req);
            if (result is null) return Unauthorized(new { message = "Credenciais inválidas." });
            return Ok(result);
        }

        /// <summary>Cria um usuário e já retorna JWT.</summary>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest req)
        {
            var result = await _auth.RegisterAsync(req);
            if (result is null) return Conflict(new { message = "Usuário já existe." });
            return Ok(result);
        }
    }
}
