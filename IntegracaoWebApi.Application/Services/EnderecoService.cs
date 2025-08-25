using Flurl.Http;
using IntegracaoWebApi.Core.Entities;
using IntegracaoWebApi.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace IntegracaoWebApi.Application.Services
{
    public class EnderecoService : IEnderecoService
    {
        private readonly ILogger<EnderecoService> _logger;
        private readonly IEnderecoRepository _enderecoRepository;
        public EnderecoService(ILogger<EnderecoService> logger,
            IEnderecoRepository enderecoRepository)
        {
            _logger = logger;
            _enderecoRepository = enderecoRepository;
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
        public async Task<List<Endereco>> GetAllAsync() =>
            await _enderecoRepository.GetAllAsync();

        public async Task<Endereco?> ImportarPorCep(string cep)
        {
            var endereco = await GetEnderecoByCepAsync(cep);

            if (endereco is null)
                return null;

            await _enderecoRepository.AddAsync(endereco);

            var persisted = (await _enderecoRepository
                                .GetAllAsync())
                                .FirstOrDefault(e => e.Cep == cep);
            return persisted;
        }
    }
}
