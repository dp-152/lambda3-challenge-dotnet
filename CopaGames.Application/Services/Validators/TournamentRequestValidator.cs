using System.Text.RegularExpressions;
using CopaGames.Domain.DTO.Tournament;
using FluentValidation;

namespace CopaGames.Application.Services.Validators;

public class TournamentRequestValidator : AbstractValidator<GameTournamentRequestDTO>
{
    public TournamentRequestValidator()
    {
        RuleFor(x => x.GameIds).NotEmpty().Must(list => list.Count() == 8).WithMessage("GameIds must have exactly 8 entries");
        RuleForEach(x => x.GameIds).NotEmpty().Matches(new Regex(@"^/.*/.*$")).WithMessage("Wrong GameId format");
    }
}
