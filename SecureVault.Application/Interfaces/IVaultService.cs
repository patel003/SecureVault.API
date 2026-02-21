using SecureVault.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecureVault.Application.Interfaces
{
    public interface IVaultService
    {
        Task AddItemAsync(int userId, CreateVaultItemDTO dto);
        Task<List<VaultItemResponseDTO>> GetMyItemsAsync(int userId);
        Task UpdateAsync(int id, int userId, CreateVaultItemDTO dto);

        Task DeleteAsync(int userId, int id);

        Task<List<VaultItemResponseDTO>> SearchAsync(int userId, string? text, string? category);

    }

}
