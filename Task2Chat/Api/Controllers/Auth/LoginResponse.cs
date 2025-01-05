using Task2Chat.Common.Api;

namespace Task2Chat.Api.Controllers.Auth
{
    public class LoginResponse : AbstractApiResponse<string>
    {
        public override string? Response { get; set; }
    }
}
