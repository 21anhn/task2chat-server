using Task2Chat.Core.Models;

namespace Task2Chat.Core.Services.Auth
{
    public interface IAuthService
    {
        Task<string> Login(string username, string password);
        Task<string> Register(User user, string password);
    }
}
