using Microsoft.AspNetCore.Mvc;

namespace Task2Chat.Common.Api
{
    public abstract class AbstractApiController<TRequest, TResponse> : ControllerBase
    {
    }
}
