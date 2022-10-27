using CopaGames.Domain.DTO.External.GamesApi;

namespace CopaGames.Application.External.Interfaces;

public interface IGamesApi
{
    Task<IEnumerable<GamesApiResponseDTO>> GetGameList();
}
