using IntegracaoWebApi.Core.Entities;

namespace IntegracaoWebApi.Core.Interfaces
{
    public interface IBrasilApiService
    {
        Task<List<Banco>> GetBancosAsync();
        Task<Banco?> GetBancoByCodeAsync(int code);
        Task<Endereco?> GetEnderecoByCepAsync(string cep);
    }
}
