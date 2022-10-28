using CopaGames.Domain.DTO.External.GamesApi;
using CopaGames.Domain.DTO.Game;

namespace CopaGames.Domain.DTO.GamesList;

public class GamesListResponseDTO : GameDTO
{
    public static implicit operator GamesApiResponseDTO(GamesListResponseDTO data)
    {
        return new GamesApiResponseDTO
        {
            Id = data.Id,
            Titulo = data.Title,
            Nota = data.Score,
            Ano = data.Year,
            UrlImagem = data.ImageUrl,
        };
    }
}
