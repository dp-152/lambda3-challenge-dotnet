using CopaGames.Domain.DTO.Game;
using CopaGames.Domain.DTO.GamesList;

namespace CopaGames.Domain.DTO.External.GamesApi;

public class GamesApiResponseDTO
{
    public string Id { get; set; }
    public string Titulo { get; set; }
    public double Nota { get; set; }
    public int Ano { get; set; }
    public string UrlImagem { get; set; }

    public static implicit operator GamesListResponseDTO(GamesApiResponseDTO data)
    {
        return new GamesListResponseDTO
        {
            Id = data.Id,
            Title = data.Titulo,
            Score = data.Nota,
            Year = data.Ano,
            ImageUrl = data.UrlImagem,
        };
    }

    public static implicit operator GameDTO(GamesApiResponseDTO data)
    {
        return new GameDTO()
        {
            Id = data.Id,
            Title = data.Titulo,
            Score = data.Nota,
            Year = data.Ano,
            ImageUrl = data.UrlImagem,
        };
    }
}