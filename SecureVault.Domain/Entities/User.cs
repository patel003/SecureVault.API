namespace SecureVault.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }   // PRIMARY KEY

        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }

        public ICollection<VaultItem>? VaultItems { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

    }
}
