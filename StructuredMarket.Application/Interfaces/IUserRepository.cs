﻿using StructuredMarket.Application.Repositories;
using StructuredMarket.Domain.Entities;
namespace StructuredMarket.Application.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> ExistsByEmailAsync(string email);
    }
}
