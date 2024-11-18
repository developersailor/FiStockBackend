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
}