using FiStockBackend.Models;

namespace FiStockBackend.Services.Interfaces;

public interface IAuthService
{
    public interface IAuthService
    {
        string Authenticate(string username, string password);
        RegistrationResult Register(string username, string password);
    }
}