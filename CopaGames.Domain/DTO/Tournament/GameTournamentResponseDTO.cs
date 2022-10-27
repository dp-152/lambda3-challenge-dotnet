using CopaGames.Domain.DTO.Game;

namespace CopaGames.Domain.DTO.Tournament;

public class GameTournamentResponseDTO
{
    public GameDTO Winner { get; set; }
    public GameDTO RunnerUp { get; set; }
}
