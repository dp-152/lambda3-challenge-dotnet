using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using CopaGames.Application.External.Interfaces;
using CopaGames.Application.Services;
using CopaGames.Domain.DTO.External.GamesApi;
using CopaGames.Domain.DTO.Tournament;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace CopaGames.Tests.Application.Services;

[TestFixture]
public class GameService_TournamentShould
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
    public async Task ReturnValid__WithValidInput()
    {
        var tournamentResult = await GetTournamentResultFromInputData("valid-tournament.json");
        Assert.IsNotNull(tournamentResult);
        Assert.IsNotNull(tournamentResult.Winner);
        Assert.IsNotNull(tournamentResult.RunnerUp);
    }

    [Test]
    public async Task ReturnValid__ForScoreComparison()
    {
        var tournamentResult = await GetTournamentResultFromInputData("tournament-by-score.json");
        AssertDefaultTournamentWinners(tournamentResult);
    }

    [Test]
    public async Task ReturnValid__ForYearComparison()
    {
        var tournamentResult = await GetTournamentResultFromInputData("tournament-by-year.json");
        AssertDefaultTournamentWinners(tournamentResult);
    }

    [Test]
    public async Task ReturnValid__ForNameComparison()
    {
        var tournamentResult = await GetTournamentResultFromInputData("tournament-by-name.json");
        AssertDefaultTournamentWinners(tournamentResult);
        
    }

    private void AssertDefaultTournamentWinners(GameTournamentResponseDTO tournamentResult)
    {
        Assert.IsNotNull(tournamentResult);
        Assert.IsNotNull(tournamentResult.Winner);
        Assert.IsNotNull(tournamentResult.RunnerUp);
        Assert.AreNotEqual(tournamentResult.Winner, tournamentResult.RunnerUp);
        Assert.AreEqual(tournamentResult.Winner.Id, "/game/8");
        Assert.AreEqual(tournamentResult.RunnerUp.Id, "/game/6");
    } 

    private async Task<GameTournamentResponseDTO> GetTournamentResultFromInputData(string dataFileName)
    {
        using var reader = new StreamReader(Path.Join(_assetsPath, dataFileName));
        var jsonString = await reader.ReadToEndAsync();
        var gameList = JsonConvert.DeserializeObject<List<GamesApiResponseDTO>>(jsonString);
        if (gameList is null)
            throw new ArgumentNullException(nameof(gameList));
        
        _mockGamesApi.Setup(gamesApi => gamesApi.GetGameList()).ReturnsAsync(gameList);

        var gameTournamentRequest = new GameTournamentRequestDTO
        {
            GameIds = new List<string>
                { "/game/1", "/game/2", "/game/3", "/game/4", "/game/5", "/game/6", "/game/7", "/game/8" }
        };

        return await _gameService.RunTournament(gameTournamentRequest);
    }
}