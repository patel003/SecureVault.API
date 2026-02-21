using Microsoft.EntityFrameworkCore;
using SecureVault.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Text;

namespace SecureVault.Persistence
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {
            
        } 

        public DbSet<User> Users { get; set; }
        public DbSet<VaultItem> VaultItems { get; set; }
    }
}
