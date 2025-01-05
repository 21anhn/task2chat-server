using Microsoft.AspNetCore.Identity;
using Task2Chat.Common.Helpers;
using Task2Chat.Core.Models;
using Task2Chat.Infrastructure.UnitOfWork;

namespace Task2Chat.Core.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="userManager"></param>
        public AuthService(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<string> Login(string username, string password)
        {
            var identityUser = await _userManager.FindByNameAsync(username);
            if (identityUser == null) return string.Empty;
            if(!(await _userManager.CheckPasswordAsync(identityUser, password))) return string.Empty;

            var roleName = (await _userManager.GetRolesAsync(identityUser)).FirstOrDefault();
            if (string.IsNullOrEmpty(roleName)) return string.Empty;

            var token = JwtHelper.GenerateToken(username, roleName);

            return string.IsNullOrEmpty(token) ? string.Empty : token;
        }

        public async Task<string> Register(User user, string password)
        {
            var identityUser = new IdentityUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = user.Email,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

            // Add user to AuthDb
            await _userManager.AddToRoleAsync(identityUser, "User"); // Default role
            await _userManager.CreateAsync(identityUser, password);
            var repository = _unitOfWork.GetRepository<User, Guid>();

            // Add user to ApplicationDb
            await repository.AddAsync(user);

            // Get token
            var token = JwtHelper.GenerateToken(user.Email, "User");
            return string.IsNullOrEmpty(token) ? string.Empty : token;
        }
    }
}
