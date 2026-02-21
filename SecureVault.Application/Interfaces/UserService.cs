using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SecureVault.Application.DTOs;
using SecureVault.Application.Interfaces;
using SecureVault.Domain.Entities;
using SecureVault.Persistence;
using System;
using System.Collections.Generic;
using System.Security.Claims;





namespace SecureVault.Application.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IJWTService _jwtService;


        public UserService(ApplicationDbContext context, IJWTService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }


        public async Task<User> CreateUserAsync(CreateUserDto dto)
        {
            dto.Normalize();

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var user = new User()
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = hashedPassword
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<List<User>> GetAllUserAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<string> LoginAsync(LoginDTO dto)
        {

           
            dto.Normalize();

            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == dto.Email);
            if (user == null)
            {
                return null;
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if (!isPasswordValid)
            {
                return null;
            }

            return _jwtService.GenerateToken(user.Email, user.Id.ToString());
         
        }
    }
}
