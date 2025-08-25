using IntegracaoWebApi.Core.Entities;

namespace IntegracaoWebApi.Core.Interfaces
{
    public interface IBancoRepository
    {
        Task AddRangeAsync(List<Banco> bancos);
        Task<List<Banco>> GetAllAsync();
    }
}
