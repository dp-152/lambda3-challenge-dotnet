using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CopaGames.Application.External.Interfaces;
using CopaGames.Application.Services;
using CopaGames.Domain.DTO.External.GamesApi;
using CopaGames.Domain.DTO.GamesList;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace CopaGames.Tests.Application.Services;

[TestFixture]
public class GameService_GetAllGamesShould
{
    private GameService _gameService;
    private Mock<IGamesApi> _mockGamesApi;

    private readonly string _assetsPath = Path.Join(
        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
        "Assets", "ServiceAssets"
    );

    [SetUp]
    public void SetUp()
    {
        _mockGamesApi = new Mock<IGamesApi>();
        _gameService = new GameService(_mockGamesApi.Object);
    }

    [Test]
    public async Task ReturnValidList()
    {
        using var reader = new StreamReader(Path.Join(_assetsPath, "game-list.json"));
        var jsonString = await reader.ReadToEndAsync();
        var gameList = JsonConvert.DeserializeObject<List<GamesApiResponseDTO>>(jsonString);
        if (gameList is null)
            throw new ArgumentNullException(nameof(gameList));
        
        _mockGamesApi.Setup(gamesApi => gamesApi.GetGameList()).ReturnsAsync(gameList);

        var comparisonSubject = gameList.Select(game => (GamesListResponseDTO) game).ToList();
        
        var gameListResult = (await _gameService.GetAllGames()).ToList();

        gameListResult.Should().NotBeNull();
        gameListResult.Should().BeEquivalentTo(comparisonSubject);
    }
}
