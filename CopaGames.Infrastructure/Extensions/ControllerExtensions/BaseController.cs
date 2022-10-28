using CopaGames.Domain.Enums.CommandResult;
using CopaGames.Infrastructure.Extensions.HttpMessages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace CopaGames.Infrastructure.Extensions.ControllerExtensions;

[ApiController]
[ApiVersion("1")]
[Route("api/{version:apiVersion}/[controller]")]
[Produces("application/json")]
public class BaseController : Controller
{
    private readonly ILogger _logger;

    protected BaseController(ILogger logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    protected IActionResult Result(BaseResponse? response)
    {
        response ??= new BaseResponse
        {
            Message = "An unknown error ocurred",
            ErrorDetails = "API Error: No response",
            ResultType = EResultType.InternalServerError,
        };

        response.RequestId = HttpContext.TraceIdentifier;

        _logger.LogInformation("response: {@response}", response);

        switch (response.ResultType)
        {
            case EResultType.Ok:
                return Ok(response);
            case EResultType.BadRequest:
                return BadRequest(response);
            case EResultType.ResourceNotFound:
                return NotFound(response);
            case EResultType.InternalServerError:
            default:
                return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!ModelState.IsValid)
        {
            var response = new BaseResponse<string>
            {
                Message = "error",
                ErrorDetails = "Falied model validation",
                ErrorList = context.ModelState
                    .Where(modelState => modelState.Value is not null && modelState.Value.Errors.Any())
                    .Select(modelError =>
                        $"{modelError.Key}: {modelError.Value!.Errors.Aggregate("", (prev, errorDetail) => prev + errorDetail.ErrorMessage + " ")}"),
                ResultType = EResultType.BadRequest,
            };
            context.Result = Result(response);
        }
        _logger.LogError("request: {@request}", context.ActionArguments);
        base.OnActionExecuting(context);
    }
}
