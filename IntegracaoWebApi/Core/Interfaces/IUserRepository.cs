﻿using IntegracaoWebApi.Core.Entities;

namespace IntegracaoWebApi.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task AddAsync(User user);
    }
}
