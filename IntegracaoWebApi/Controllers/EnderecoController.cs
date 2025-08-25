using IntegracaoWebApi.Core.Entities;
using IntegracaoWebApi.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IntegracaoWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnderecosController : ControllerBase
    {
        private readonly IBrasilApiService _brasilApiService;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly ILogger<EnderecosController> _logger;

        public EnderecosController(
            IBrasilApiService brasilApiService,
            IEnderecoRepository enderecoRepository,
            ILogger<EnderecosController> logger)
        {
            _brasilApiService = brasilApiService;
            _enderecoRepository = enderecoRepository;
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
            var enderecos = await _enderecoRepository.GetAllAsync();
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
                var endereco = await _brasilApiService.GetEnderecoByCepAsync(cep);
                if (endereco is null)
                    return NotFound(new { message = $"CEP '{cep}' não encontrado na BrasilAPI." });

                await _enderecoRepository.AddAsync(endereco);

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
        /// <param name="code">Código do Endereço</param>
        /// <returns>Endereço importado</returns>
        [HttpPost("importar/{cep}")]
        public async Task<IActionResult> ImportarPorCep(string cep)
        {
            try
            {
                var endereco = await _brasilApiService.GetEnderecoByCepAsync(cep);
                if (endereco is null)
                    return NotFound(new { message = $"CEP '{cep}' não encontrado na BrasilAPI." });

                await _enderecoRepository.AddAsync(endereco);

                var todos = await _enderecoRepository.GetAllAsync();
                var persisted = todos.FirstOrDefault(e => e.Cep == cep) ?? endereco;

                return CreatedAtAction(nameof(GetByCep), new { cep = persisted.Cep }, persisted);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao importar CEP {cep} da BrasilAPI.", cep);
                return StatusCode(502, new { message = "Erro ao consumir a BrasilAPI de CEP." });
            }
        }
    }
}
