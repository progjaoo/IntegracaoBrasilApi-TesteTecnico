using IntegracaoWebApi.Application.DTOs;
using IntegracaoWebApi.Core.Entities;

namespace IntegracaoWebApi.Core.Interfaces
{
    public interface IBancoService
    {
        Task<Banco?> GetBancoByCodeAsync(int code);
        Task<List<Banco>> GetBancosAsync();
        Task<Banco?> ImportarBancoPorCodigo(int code);
    }
}
