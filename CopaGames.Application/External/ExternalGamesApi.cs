using CopaGames.Application.External.Interfaces;
using CopaGames.Domain.DTO.External.GamesApi;
using Newtonsoft.Json;

namespace CopaGames.Application.External;

public class ExternalGamesApi : IGamesApi
{
    private readonly IHttpClientFactory _clientFactory;

    public ExternalGamesApi(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
    }
    
    public async Task<IEnumerable<GamesApiResponseDTO>> GetGameList()
    {
        HttpRequestMessage httpRequest = new(HttpMethod.Get, new Uri("/api/Competidores?copa=games", UriKind.Relative));
        using var client = _clientFactory.CreateClient("CopaGamesApi");
        var response = await client.SendAsync(httpRequest);
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Error sending request: {response.StatusCode}: {response.ReasonPhrase} - {content}");
        }

        var result = JsonConvert.DeserializeObject<IEnumerable<GamesApiResponseDTO>>(content);

        if (result is null)
        {
            throw new Exception("Empty response from API");
        }

        return result;
    }
}
