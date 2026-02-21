using SecureVault.Application.DTOs;
using SecureVault.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecureVault.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(CreateUserDto dto);
        Task<List<User>> GetAllUserAsync();
        Task<string> LoginAsync(LoginDTO dto);

    }
}
