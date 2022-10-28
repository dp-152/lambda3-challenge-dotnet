using CopaGames.Domain.Enums.CommandResult;
using CopaGames.Infrastructure.Extensions.ControllerExtensions;
using CopaGames.Infrastructure.Extensions.HttpMessages;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CopaGames.API.Controllers;

public class ErrorHandlerController : BaseController
{
    public ErrorHandlerController(ILogger<ErrorHandlerController> logger) : base(logger)
    {
    }

    [Route("/error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult HandleError([FromServices] IHostEnvironment hostEnvironment)
    {
        var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var response = new BaseResponse
        {
            Message = "An internal error ocurred",
            ResultType = EResultType.InternalServerError,
            ErrorDetails = exceptionHandlerFeature?.Error.Message ?? null,
        };

        if (hostEnvironment.IsDevelopment())
            response.ErrorStack = exceptionHandlerFeature?.Error.StackTrace ?? null;

        return Result(response);
    }
}
