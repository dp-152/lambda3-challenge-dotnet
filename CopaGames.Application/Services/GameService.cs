using CopaGames.Application.External.Interfaces;
using CopaGames.Application.Services.Interfaces;
using CopaGames.Domain.DTO.Game;
using CopaGames.Domain.DTO.Tournament;

namespace CopaGames.Application.Services;

public class GameService : IGameService
{
    private readonly IGamesApi _gamesApi;
    public GameService(IGamesApi gamesApi)
    {
        _gamesApi = gamesApi ?? throw new ArgumentNullException(nameof(gamesApi));
    }

    public async Task<IEnumerable<GameDTO>> GetAllGames()
    {
        var result = await _gamesApi.GetGameList();
        return result.Select(game => new GameDTO
        {
            Id = game.Id,
            Title = game.Titulo,
            Score = game.Nota,
            Year = game.Ano,
            ImageUrl = game.UrlImagem,
        });
    }

    public Task<IEnumerable<GameDTO>> RunTournament(GameTournamentRequestDTO request)
    {
        throw new NotImplementedException();
    }
}