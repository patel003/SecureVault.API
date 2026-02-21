using System;
using System.Collections.Generic;
using System.Text;

namespace SecureVault.Application.DTOs
{
    public class LoginDTO
    {
        public string? Email { get; set; }
        public string? Password { get; set; }


        public void Normalize()
        {
            Email = Email?.Trim().ToLower();
            Password = Password?.Trim();
        }
    }

}
