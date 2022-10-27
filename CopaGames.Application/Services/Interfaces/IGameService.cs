using CopaGames.Domain.DTO.GamesList;
using CopaGames.Domain.DTO.Tournament;

namespace CopaGames.Application.Services.Interfaces;

public interface IGameService
{
    Task<IEnumerable<GamesListResponseDTO>> GetAllGames();
    Task<GameTournamentResponseDTO> RunTournament(GameTournamentRequestDTO request);
}
