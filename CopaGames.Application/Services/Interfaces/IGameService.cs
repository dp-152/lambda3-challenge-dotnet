using CopaGames.Domain.DTO.Game;
using CopaGames.Domain.DTO.Tournament;

namespace CopaGames.Application.Services.Interfaces;

public interface IGameService
{
    Task<IEnumerable<GameDTO>> GetAllGames();
    Task<IEnumerable<GameDTO>> RunTournament(GameTournamentRequestDTO request);
}
