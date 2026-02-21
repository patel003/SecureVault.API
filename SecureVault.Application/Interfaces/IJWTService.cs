using System;
using System.Collections.Generic;
using System.Text;

namespace SecureVault.Application.Interfaces
{
    public interface IJWTService
    {
        string GenerateToken(string email, string userId);
    }
}