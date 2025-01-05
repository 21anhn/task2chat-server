using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task2Chat.Common.Api;
using Task2Chat.Common.Constants;
using Task2Chat.Core.Services.Auth;

namespace Task2Chat.Api.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class LoginController : AbstractApiController<LoginRequest, LoginResponse>
    {
        private readonly IAuthService _service;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        public LoginController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _service.Login(request.UserName, request.Password);
            return string.IsNullOrEmpty(token) ? Unauthorized(Messages.GetMessageById(Messages.E00003)) : Ok(token);
        }
    }
}
