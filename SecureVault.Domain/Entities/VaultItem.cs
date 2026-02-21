namespace SecureVault.Domain.Entities
{
    public class VaultItem
    {
        public int Id { get; set; }   // PRIMARY KEY

        public string? Title { get; set; }
        public string? EncryptedData { get; set; }
        public string Category { get; set; } = "Other";

        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
