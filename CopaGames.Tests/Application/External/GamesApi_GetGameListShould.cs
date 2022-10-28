using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CopaGames.Application.External;
using CopaGames.Application.External.Interfaces;
using CopaGames.Domain.DTO.External.GamesApi;
using CopaGames.Infrastructure.Extensions.Utility;
using FluentAssertions;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using NUnit.Framework;

namespace CopaGames.Tests.Application.External;

[TestFixture]
public class GamesApi_GetGameListShould
{
    private IGamesApi _gamesApi;
    private HttpResponseMessage _mockHttpResponseMessage;

    private readonly string _assetsPath = Path.Join(
        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
        "Assets", "ExternalServiceAssets"
    );
    
    [SetUp]
    public void SetUp()
    {

        _mockHttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
        var mockClientHandler = new Mock<DelegatingHandler>();
        mockClientHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(_mockHttpResponseMessage);
        mockClientHandler.As<IDisposable>().Setup(client => client.Dispose());

        var mockHttpClient = new HttpClient(mockClientHandler.Object)
        {
            BaseAddress = new Uri("https://example.com"),
        };
        var mockHttpClientFactory = new Mock<IHttpClientFactory>(MockBehavior.Strict);
        mockHttpClientFactory.Setup(clientFactory => clientFactory.CreateClient(It.IsAny<string>())).Returns(mockHttpClient);
        
        _gamesApi = new ExternalGamesApi(mockHttpClientFactory.Object);
    }

    [Test]
    public async Task ReturnGameList()
    {
        var (apiResponseString, comparisonSubject) =
            await FileRead.ReadJsonFromFile<List<GamesApiResponseDTO>>(Path.Join(_assetsPath, "games-api-response.json"));

        _mockHttpResponseMessage.Content = new StringContent(apiResponseString);

        var gameList = (await _gamesApi.GetGameList()).ToList();

        gameList.Should().NotBeNull();
        gameList.Should().BeEquivalentTo(comparisonSubject);
    }
}
