using IntegracaoWebApi.Core.Entities;

namespace IntegracaoWebApi.Core.Interfaces
{
    public interface IBancoRepository
    {
        Task AddRangeAsync(List<Banco> bancos);
        Task<List<Banco>> GetAllAsync();
        Task<List<Banco>> BuscarPorNomeAproximado(string nome);
        Task<List<Banco>> GetBancosViaSql();

    }
}
