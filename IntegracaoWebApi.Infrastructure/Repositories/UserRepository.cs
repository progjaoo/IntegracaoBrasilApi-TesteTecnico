using IntegracaoWebApi.Core.Entities;
using IntegracaoWebApi.Core.Interfaces;
using IntegracaoWebApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace IntegracaoWebApi.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext ctx) => _context = ctx;

        public async Task<User?> GetByUsernameAsync(string username) =>
            await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Username == username);

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
