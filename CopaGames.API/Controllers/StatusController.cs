using CopaGames.Domain.Enums.CommandResult;
using CopaGames.Infrastructure.Extensions.ControllerExtensions;
using CopaGames.Infrastructure.Extensions.HttpMessages;
using Microsoft.AspNetCore.Mvc;

namespace CopaGames.API.Controllers;

public class StatusController : BaseController
{
    private readonly ILogger<StatusController> _logger;

    public StatusController(ILogger<StatusController> logger) : base(logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetStatus")]
    public IActionResult Get()
    {
        BaseResponse response = new()
        {
            Message = "Ok",
            ResultType = EResultType.Ok
        };
        return Result(response);
    }
}
