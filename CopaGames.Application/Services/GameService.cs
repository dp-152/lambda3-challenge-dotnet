using CopaGames.Application.External.Interfaces;
using CopaGames.Application.Services.Interfaces;
using CopaGames.Domain.DTO.Game;
using CopaGames.Domain.DTO.GamesList;
using CopaGames.Domain.DTO.Tournament;
using CopaGames.Domain.Enums.Tournament;

namespace CopaGames.Application.Services;

public class GameService : IGameService
{
    private readonly IGamesApi _gamesApi;
    public GameService(IGamesApi gamesApi)
    {
        _gamesApi = gamesApi ?? throw new ArgumentNullException(nameof(gamesApi));
    }

    public async Task<IEnumerable<GamesListResponseDTO>> GetAllGames()
    {
        var result = await _gamesApi.GetGameList();
        return result.Select(game => new GamesListResponseDTO
        {
            Id = game.Id,
            Title = game.Titulo,
            Score = game.Nota,
            Year = game.Ano,
            ImageUrl = game.UrlImagem,
        });
    }

    public async Task<GameTournamentResponseDTO> RunTournament(GameTournamentRequestDTO request)
    {
        var allGames = await _gamesApi.GetGameList();
        var contestants = allGames
            .Where(game => request.GameIds.Contains(game.Id))
            .Select(game => new GameDTO
            {
                Id = game.Id,
                Title = game.Titulo,
                Score = game.Nota,
                Year = game.Ano,
                ImageUrl = game.UrlImagem,
            }).ToList();

        if (contestants.Count() != request.GameIds.Count())
        {
            throw new Exception(
                $"Not enough contestants to start a tournament: only {contestants.Count()} games found out of {request.GameIds.Count()} IDs provided");
        }

        var keys = GenerateTournamentKeys(contestants, ETournamentKeyingMode.Mirrored);

        while (keys.Count() > 1)
        {
            var roundWinners = RunRound(keys);
            keys = GenerateTournamentKeys(roundWinners, ETournamentKeyingMode.Adjacent);
        }

        var finals = SelectWinner(keys[0]);

        return new GameTournamentResponseDTO
        {
            Winner = finals.Item1,
            RunnerUp = finals.Item2,
        };
    }

    private List<Tuple<GameDTO, GameDTO>> GenerateTournamentKeys(List<GameDTO> games, ETournamentKeyingMode keyingMode)
    {
        Func<int, int, int> rightValueMethod = (_, _) => 0;
        Func<int, int> iterationCeiling = _ => 0;
        int increment = 1;

        switch (keyingMode)
        {
            case ETournamentKeyingMode.Adjacent:
                rightValueMethod = (left, _) => left + 1;
                iterationCeiling = count => count - 1;
                increment = 2;
                break;

            case ETournamentKeyingMode.Mirrored:
                rightValueMethod = (left, count) => count - 1 - left;
                iterationCeiling = count => count / 2;
                break;
        }
        
        List<Tuple<GameDTO, GameDTO>> result = new();

        for (int left = 0; left < iterationCeiling(games.Count()); left += increment)
        {
            var right = rightValueMethod(left, games.Count());
            result.Add(new Tuple<GameDTO, GameDTO>(games[left], games[right]));
        }

        return result;
    }

    private List<GameDTO> RunRound(List<Tuple<GameDTO, GameDTO>> keys)
    {
        List<GameDTO> roundWinners = new();

        foreach (var match in keys)
        {
            roundWinners.Add(SelectWinner(match).Item1);
        }

        return roundWinners;
    }

    private Tuple<GameDTO, GameDTO> SelectWinner(Tuple<GameDTO, GameDTO> contestants)
    {
        return CompareByScore(contestants) ?? CompareByYear(contestants) ?? CompareByTitle(contestants);
    }

    private Tuple<GameDTO, GameDTO>? CompareByScore(Tuple<GameDTO, GameDTO> contestants)
    {
        if (contestants.Item1.Score > contestants.Item2.Score)
            return new Tuple<GameDTO, GameDTO>(contestants.Item1, contestants.Item2);

        if (contestants.Item1.Score < contestants.Item2.Score)
            return new Tuple<GameDTO, GameDTO>(contestants.Item2, contestants.Item1);

        return null;
    }

    private Tuple<GameDTO, GameDTO>? CompareByYear(Tuple<GameDTO, GameDTO> contestants)
    {
        if (contestants.Item1.Year > contestants.Item2.Year)
            return new Tuple<GameDTO, GameDTO>(contestants.Item1, contestants.Item2);

        if (contestants.Item1.Year < contestants.Item2.Year)
            return new Tuple<GameDTO, GameDTO>(contestants.Item2, contestants.Item1);

        return null;
    }

    private Tuple<GameDTO, GameDTO> CompareByTitle(Tuple<GameDTO, GameDTO> contestants)
    {
        var compare = string.Compare(contestants.Item1.Title, contestants.Item2.Title, StringComparison.Ordinal);
        if (compare >= 0)
            return new Tuple<GameDTO, GameDTO>(contestants.Item1, contestants.Item2);;

        return new Tuple<GameDTO, GameDTO>(contestants.Item2, contestants.Item1);
    }
}
