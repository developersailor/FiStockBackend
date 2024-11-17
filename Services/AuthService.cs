using FiStockBackend.Context;
using FiStockBackend.Models;
using FiStockBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;

namespace FiStockBackend.Services;
public class AuthService : IAuthService
{
    private readonly StockTrackingDbContext _context;

    public AuthService(StockTrackingDbContext context)
    {
        _context = context;
    }

    public async Task<User?> AuthenticateAsync(string username, string password)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);

        if (user == null)
        {
            return null;
        }

        // Diğer işlemler...

        return user;
    }

    public async Task<string> GenerateJwtTokenAsync(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("your_secret_key_here");

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.UserId.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}