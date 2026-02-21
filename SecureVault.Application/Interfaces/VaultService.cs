using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureVault.Application.Interfaces;
using SecureVault.Application.DTOs;
using SecureVault.Domain.Entities;
using SecureVault.Persistence;
using System;
using System.Collections.Generic;
using System.Text;


namespace SecureVault.Application.Interfaces
{
    public class VaultService : IVaultService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEncryptionService _encryption;
        public VaultService(ApplicationDbContext context, IEncryptionService encryption)
        {
            _context = context;
            _encryption = encryption;
        }

        public async Task AddItemAsync(int userId, CreateVaultItemDTO dto)
        {
            var encrypted = _encryption.Encrypt(dto.Data);

            var item = new VaultItem
            {
                Title = dto.Title,
                EncryptedData = encrypted,
                Category = dto.Category,
                UserId = userId
            };


            _context.VaultItems.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task<List<VaultItemResponseDTO>> GetMyItemsAsync(int userId)
        {
            return await _context.VaultItems
                .Where(x => x.UserId == userId)
                .Select(x => new VaultItemResponseDTO
                {
                    Id = x.Id,
                    Title = x.Title ?? "",
                    Category = x.Category,
                    Data = _encryption.Decrypt(x.EncryptedData)


                })
                .ToListAsync();
        }

        public async Task UpdateAsync(int id, int userId, CreateVaultItemDTO dto)
        {
            var item = await _context.VaultItems
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

            if (item == null)
            {
                throw new UnauthorizedAccessException("Item not found or access denied");
            }
              

            // Encrypt new password
            var encrypted = _encryption.Encrypt(dto.Data);

            item.Title = dto.Title;
            item.EncryptedData = encrypted;

            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int userId, int id)
        {
            var item = await _context.VaultItems
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

            if (item == null)
                throw new UnauthorizedAccessException();

            _context.VaultItems.Remove(item);
            await _context.SaveChangesAsync();
        }


        public async Task<List<VaultItemResponseDTO>> SearchAsync(int userId, string? text, string? category)
        {
            var query = _context.VaultItems.Where(x => x.UserId == userId);

            // search by title
            if (!string.IsNullOrWhiteSpace(text))
                query = query.Where(x => x.Title.Contains(text));

            // filter by category
            if (!string.IsNullOrWhiteSpace(category))
                query = query.Where(x => x.Category == category);

            var items = await query.ToListAsync();

            return items.Select(x => new VaultItemResponseDTO
            {
                Id = x.Id,
                Title = x.Title,
                Category = x.Category,
                Data = _encryption.Decrypt(x.EncryptedData)
            }).ToList();
        }

    }

}
