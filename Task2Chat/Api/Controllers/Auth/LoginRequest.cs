using Task2Chat.Common.Api;

namespace Task2Chat.Api.Controllers.Auth
{
    public class LoginRequest : AbstractApiRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
