using IntegracaoWebApi.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IntegracaoWebApi.Core.Interfaces
{
    public interface IEnderecoService
    {
        Task<Endereco?> GetEnderecoByCepAsync(string cep);
        Task<List<Endereco>> GetAllAsync();
        Task<Endereco?> ImportarPorCep(string cep);
    }
}
