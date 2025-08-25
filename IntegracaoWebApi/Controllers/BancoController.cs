using IntegracaoWebApi.Core.Entities;
using IntegracaoWebApi.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IntegracaoWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BancoController : ControllerBase
    {
        private readonly IBrasilApiService _brasilApiService;
        private readonly IBancoRepository _bancoRepository;
        private readonly ILogger<BancoController> _logger;

        public BancoController(IBrasilApiService brasilApiService,
            IBancoRepository bancoRepository, ILogger<BancoController> logger)
        {
            _brasilApiService = brasilApiService;
            _bancoRepository = bancoRepository;
            _logger = logger;
        }
        /// <summary>
        /// Retorna todos os bancos disponíveis na BrasilAPI.
        /// </summary>
        /// <returns>Lista de bancos</returns>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Banco>>> GetAll()
        {
            var bancos = await _brasilApiService.GetBancosAsync();
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
            try
            {
                var banco = await _brasilApiService.GetBancoByCodeAsync(code);
                if (banco is null)
                    return NotFound(new { message = $"Banco com código '{code}' não encontrado na BrasilAPI." });

                return Ok(banco);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar banco código {code} na BrasilAPI.", code);
                return StatusCode(502, new { message = "Erro ao consumir a BrasilAPI de bancos." });
            }
        }
        /// <summary>
        /// Importa um banco da BrasilAPI para o banco de dados local.
        /// </summary>
        /// <param name="code">Código do banco</param>
        /// <returns>Banco importado</returns>
        [HttpPost("importar/{code}")]
        public async Task<IActionResult> ImportarPorCode(int code)
        {
            try
            {
                var banco = await _brasilApiService.GetBancoByCodeAsync(code);
                if (banco is null)
                    return NotFound(new { message = $"Banco com código '{code}' não encontrado na BrasilAPI." });

                await _bancoRepository.AddRangeAsync(new List<Banco> { banco });

                var todos = await _bancoRepository.GetAllAsync();
                var persisted = todos.FirstOrDefault(b => b.Codigo == code) ?? banco;

                return CreatedAtAction(nameof(GetByCode), new { code = persisted.Codigo }, persisted);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao importar banco código {code} da BrasilAPI.", code);
                return StatusCode(502, new { message = "Erro ao consumir a BrasilAPI de bancos." });
            }
        }
    }
}
