using IntegracaoWebApi.Core.Entities;

namespace IntegracaoWebApi.Core.Interfaces
{
    public interface IEnderecoRepository
    {
        Task AddAsync(Endereco endereco);
        Task<List<Endereco>> GetAllAsync();
    }
}
