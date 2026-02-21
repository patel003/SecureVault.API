using System;
using System.Collections.Generic;
using System.Text;

namespace SecureVault.Application.Interfaces
{
    public interface IEncryptionService
    {
        string Encrypt(string text);
        string Decrypt(string cipher);
    }
}
