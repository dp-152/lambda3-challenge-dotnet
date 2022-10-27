using CopaGames.Application.Services.Interfaces;
using CopaGames.Domain.DTO.GamesList;
using CopaGames.Domain.DTO.Tournament;
using CopaGames.Domain.Enums.CommandResult;
using CopaGames.Infrastructure.Extensions.ControllerExtensions;
using CopaGames.Infrastructure.Extensions.HttpMessages;
using Microsoft.AspNetCore.Mvc;

namespace CopaGames.API.Controllers;

public class GameController : BaseController
{
    private readonly IGameService _gameService;

    public GameController(ILogger<GameController> logger, IGameService gameService) : base(logger)
    {
        _gameService = gameService ?? throw new ArgumentNullException();
    }

    [HttpGet]
    [ProducesDefaultResponseType(typeof(BaseResponse<IEnumerable<GamesListResponseDTO>>))]
    public async Task<IActionResult> GetAllGames()
    {
        var response = new BaseResponse<IEnumerable<GamesListResponseDTO>>
        {
            Message = "ok",
            ResultType = EResultType.Ok,
            Data = await _gameService.GetAllGames(),
        };

        return Result(response);
    }

    [HttpPost]
    [ProducesDefaultResponseType(typeof(BaseResponse<GameTournamentResponseDTO>))]
    public async Task<IActionResult> RunTournament(GameTournamentRequestDTO request)
    {
        var response = new BaseResponse<GameTournamentResponseDTO>
        {
            Message = "ok",
            ResultType = EResultType.Ok,
            Data = await _gameService.RunTournament(request),
        };

        return Result(response);
    }
}