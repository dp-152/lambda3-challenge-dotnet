using CopaGames.Domain.Enums.CommandResult;

namespace CopaGames.Infrastructure.Extensions.Exceptions;

public class BadRequestException : ApiException
{
    public BadRequestException(string details) : base(EResultType.BadRequest, $"Bad request: {details}")
    {
    }
}
