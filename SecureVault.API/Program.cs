using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SecureVault.Application.Interfaces;
using SecureVault.Application.Services;
using SecureVault.Infrastructure.Services;
using SecureVault.Persistence;
using System.Text;
using SecureVault.Application.Interfaces;
using SecureVault.Application.Services;
using SecureVault.Infrastructure.Services;
using SecureVault.Application.Interfaces;
using SecureVault.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// ================= DATABASE =================
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ================= SERVICES =================
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IVaultService, VaultService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IVaultService, VaultService>();

builder.Services.AddScoped<IJWTService, JWTService>();
builder.Services.AddScoped<IEncryptionService, EncryptionService>();

builder.Services.AddControllers();

// ================= JWT =================
var jwtKey = builder.Configuration["Jwt:Key"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

builder.Services.AddAuthorization();

// ================= APP =================
var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
