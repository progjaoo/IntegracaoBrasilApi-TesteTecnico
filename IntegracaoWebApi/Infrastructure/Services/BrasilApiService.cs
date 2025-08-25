using Flurl.Http;
using IntegracaoWebApi.Core.Entities;
using IntegracaoWebApi.Core.Interfaces;

namespace IntegracaoWebApi.Infrastructure.Services
{
    public class BrasilApiService : IBrasilApiService
    {
        private readonly ILogger<BrasilApiService> _logger;

        public BrasilApiService(ILogger<BrasilApiService> logger)
        {
            _logger = logger;
        }

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
        public async Task<Endereco?> GetEnderecoByCepAsync(string cep)
        {
            try
            {
                var url = $"https://brasilapi.com.br/api/cep/v1/{cep}";
                var result = await url.GetJsonAsync<Endereco>();
                return result;
            }
            catch (FlurlHttpException ex)
            {
                _logger.LogError(ex, "Erro ao consumir BrasilAPI - cep {cep}", cep);
                throw;
            }
        }
    }
}
