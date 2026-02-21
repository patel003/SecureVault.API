using System;
using System.Collections.Generic;
using System.Text;

namespace SecureVault.Application.DTOs
{
    public class CreateVaultItemDTO
    {
        public string Title { get; set; } = null!;
        public string Data { get; set; } = null!;
        public string Category { get; set; } = "Other";

    }

}
