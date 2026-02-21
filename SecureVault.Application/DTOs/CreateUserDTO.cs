namespace SecureVault.Application.DTOs
{
    public class CreateUserDto
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }   // Plain password

        public void Normalize()
        {
            Email = Email?.Trim().ToLower();
            Password = Password?.Trim();
            FullName = FullName?.Trim();
        }

    }
}
