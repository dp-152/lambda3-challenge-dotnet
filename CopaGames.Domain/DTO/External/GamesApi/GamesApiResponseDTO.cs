namespace CopaGames.Domain.DTO.External.GamesApi;

public class GamesApiResponseDTO
{
    public string Id { get; set; }
    public string Titulo { get; set; }
    public double Nota { get; set; }
    public int Ano { get; set; }
    public string UrlImagem { get; set; }
}