using Flurl.Http;
using IntegracaoWebApi.Core.Entities;
using IntegracaoWebApi.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace IntegracaoWebApi.Application.Services
{
    public class BancoService : IBancoService
    {
        private readonly ILogger<BancoService> _logger;
        private readonly IBancoRepository _bancoRepository;
        public BancoService(ILogger<BancoService> logger,
            IBancoRepository bancoRepository)
        {
            _logger = logger;
            _bancoRepository = bancoRepository;
        }
        public async Task<List<Banco>> GetAllAsync() =>
            await _bancoRepository.GetAllAsync();
        public async Task<List<Banco>> GetBancosAsync()
        {
            try
            {
                var url = "https://brasilapi.com.br/api/banks/v1";
                var result = await url.GetJsonAsync<List<Banco>>();
                return result;
            }
            catch (FlurlHttpException ex)
            {
                _logger.LogError(ex, "Erro ao consumir BrasilAPI - bancos");
                throw;
            }
        }
        public async Task<Banco?> GetBancoByCodeAsync(int code)
        {
            try
            {
                var url = $"https://brasilapi.com.br/api/banks/v1/{code}";
                var result = await url.GetJsonAsync<Banco>();
                return result;
            }
            catch (FlurlHttpException ex)
            {
                _logger.LogError(ex, "Erro ao consumir BrasilAPI - banco code {code}", code);
                return null;
            }
        }

        public async Task<Banco?> ImportarBancoPorCodigo(int code)
        {
            var bancoApi = await GetBancoByCodeAsync(code);
            if (bancoApi == null) return null;

            await _bancoRepository.AddRangeAsync(new List<Banco> { bancoApi });

            var persisted = await GetBancoByCodeAsync(code);
            return persisted;
        }
        public async Task<List<Banco>> BuscarPorNome(string nome) =>
            await _bancoRepository.BuscarPorNomeAproximado(nome);

        public async Task<List<Banco>> GetBancosViaSql() =>
            await _bancoRepository.GetBancosViaSql();
    }
}