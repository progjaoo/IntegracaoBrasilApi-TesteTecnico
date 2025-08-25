using IntegracaoWebApi.Application.Services;
using IntegracaoWebApi.Core.Entities;
using IntegracaoWebApi.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static IntegracaoWebApi.Core.Exception.Exceptions;

namespace IntegracaoWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BancoController : ControllerBase
    {
        private readonly IBancoService _bancoService;
        private readonly ILogger<BancoController> _logger;

        public BancoController(IBancoService bancoService,
            ILogger<BancoController> logger)
        {
            _bancoService = bancoService;
            _logger = logger;
        }
        /// <summary>
        /// Retorna todos os Bancos disponíveis no Banco de Dados Local.
        /// </summary>
        /// <returns>Lista de Bancos</returns>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Banco>>> GetAllAsync()
        {
            var bancos = await _bancoService.GetAllAsync();
            return Ok(bancos);
        }
        /// <summary>
        /// Retorna todos os bancos disponíveis na BrasilAPI.
        /// </summary>
        /// <returns>Lista de bancos</returns>
        /// <response code="200">Sucesso</response>
        [HttpGet("todosBancos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Banco>>> GetAllBancos()
        {
            var bancos = await _bancoService.GetBancosAsync();
            return Ok(bancos);
        }
        /// <summary>
        /// Busca um banco pelo código na BrasilAPI.
        /// </summary>
        /// <param name="code">Código do banco</param>
        /// <returns>Banco correspondente ao código informado</returns>
        /// <response code="200">Sucesso</response>
        [HttpGet("code/{code}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Banco>> GetByCode(int code)
        {
            var banco = await _bancoService.GetBancoByCodeAsync(code);
            if (banco is null)
                throw new NotFoundException($"Banco com código '{code}' não encontrado na BrasilAPI.");

            return Ok(banco);
        }
        /// <summary>
        /// Importa um banco da BrasilAPI para o banco de dados local.
        /// </summary>
        /// <param name="code">Código do banco</param>
        /// <returns>Banco importado</returns>
        [HttpPost("importar/{code}")]
        [Authorize]
        public async Task<IActionResult> ImportarPorCode(int code)
        {
            var banco = await _bancoService.ImportarBancoPorCodigo(code);
            if (banco is null)
                throw new NotFoundException($"Banco com código '{code}' não encontrado.");

            return CreatedAtAction(nameof(GetByCode), new { code = banco.Codigo }, banco);
        }
    }
}
