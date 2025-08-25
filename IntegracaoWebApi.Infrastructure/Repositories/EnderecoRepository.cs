using IntegracaoWebApi.Core.Entities;
using IntegracaoWebApi.Core.Interfaces;
using IntegracaoWebApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace IntegracaoWebApi.Infrastructure.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly AppDbContext _context;

        public EnderecoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Endereco endereco)
        {
            var exists = await _context.Enderecos
                .AnyAsync(e => e.Cep == endereco.Cep);

            if (!exists)
            {
                _context.Enderecos.Add(endereco);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<Endereco>> GetAllAsync() =>
            await _context.Enderecos.AsNoTracking().ToListAsync();
    }
}