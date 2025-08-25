using IntegracaoWebApi.Core.Entities;
using IntegracaoWebApi.Core.Interfaces;
using IntegracaoWebApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace IntegracaoWebApi.Infrastructure.Repositories
{
    public class BancoRepository : IBancoRepository
    {
        private readonly AppDbContext _context;

        public BancoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddRangeAsync(List<Banco> bancos)
        {
            foreach (var banco in bancos)
            {
                var exists = await _context.Bancos
                    .AnyAsync(b => b.Ispb == banco.Ispb);

                if (!exists)
                {
                    _context.Bancos.Add(banco);
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<Banco>> GetAllAsync() =>
            await _context.Bancos.AsNoTracking().ToListAsync();

        public async Task<List<Banco>> BuscarPorNomeAproximado(string nome)
        {
            return await _context.Bancos
                .Where(b => EF.Functions.Like(b.Nome, $"%{nome}%"))
                .ToListAsync();
        }
        public async Task<List<Banco>> GetBancosViaSql()
        {
            return await _context.Bancos
                .FromSqlRaw("SELECT * FROM Bancos WHERE Codigo > 100 ORDER BY Nome")
                .ToListAsync();
        }

    }
}
