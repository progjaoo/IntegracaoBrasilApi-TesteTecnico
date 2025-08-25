using IntegracaoWebApi.Core.Entities;
using IntegracaoWebApi.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IntegracaoWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnderecosController : ControllerBase
    {
        private readonly IEnderecoService _enderecoService;
        private readonly ILogger<EnderecosController> _logger;

        public EnderecosController(
            IEnderecoService enderecoService,
            ILogger<EnderecosController> logger)
        {
            _enderecoService = enderecoService;
            _logger = logger;
        }
        /// <summary>
        /// Retorna todos os endereços disponíveis no Banco de Dados Local.
        /// </summary>
        /// <returns>Lista de endereços</returns>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Endereco>>> GetAll()
        {
            var enderecos = await _enderecoService.GetAllAsync();
            return Ok(enderecos);
        }
        /// <summary>
        /// Busca um endereço pelo CEP na BrasilAPI.
        /// </summary>
        /// <param name="cep">CEP do Endereço</param>
        /// <returns>Endereço correspondente ao CEP informado</returns>
        /// <response code="200">Sucesso</response>
        [HttpGet("{cep}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Endereco>> GetByCep(string cep)
        {
            try
            {
                var endereco = await _enderecoService.GetEnderecoByCepAsync(cep);
                if (endereco is null)
                    return NotFound(new { message = $"CEP '{cep}' não encontrado na BrasilAPI." });

                return Ok(endereco);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar CEP {cep} na BrasilAPI.", cep);
                return StatusCode(502, new { message = "Erro ao consumir a BrasilAPI de CEP." });
            }
        }
        /// <summary>
        /// Importa um Endereço da BrasilAPI para o banco de dados local.
        /// </summary>
        /// <param name="cep">Cep do Endereço</param>
        /// <returns>Endereço importado</returns>
        [HttpPost("importar/{cep}")]
        public async Task<IActionResult> ImportarPorCep(string cep)
        {
            try
            {
                var endereco = await _enderecoService.ImportarPorCep(cep);
                if (endereco is null)
                    return NotFound(new { message = $"CEP '{cep}' não encontrado na BrasilAPI." });

                return CreatedAtAction(nameof(GetByCep), new { cep = endereco.Cep }, endereco);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao importar CEP {cep} da BrasilAPI.", cep);
                return StatusCode(502, new { message = "Erro ao importar a BrasilAPI de CEP." });
            }
        }
    }
}