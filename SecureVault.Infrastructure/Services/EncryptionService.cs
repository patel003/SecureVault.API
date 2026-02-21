using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using SecureVault.Application.Interfaces;

namespace SecureVault.Application.Interfaces
{
    public class EncryptionService : IEncryptionService
    {
        private readonly byte[] _key;

        public EncryptionService(IConfiguration configuration)
        {
            var keyString = configuration["EncryptionSettings:Key"];
            _key = Encoding.UTF8.GetBytes(keyString);
        }

        public string Encrypt(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = _key;
            aes.GenerateIV();

            var iv = aes.IV;
            using var encryptor = aes.CreateEncryptor(aes.Key, iv);

            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            var encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

            var result = iv.Concat(encryptedBytes).ToArray();
            return Convert.ToBase64String(result);
        }

        public string Decrypt(string cipherText)
        {
            var fullCipher = Convert.FromBase64String(cipherText);

            using var aes = Aes.Create();
            aes.Key = _key;

            var iv = fullCipher.Take(16).ToArray();
            var cipher = fullCipher.Skip(16).ToArray();

            using var decryptor = aes.CreateDecryptor(aes.Key, iv);
            var decryptedBytes = decryptor.TransformFinalBlock(cipher, 0, cipher.Length);

            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}
