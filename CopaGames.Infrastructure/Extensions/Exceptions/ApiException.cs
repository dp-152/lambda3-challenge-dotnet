using CopaGames.Domain.Enums.CommandResult;

namespace CopaGames.Infrastructure.Extensions.Exceptions;

public class ApiException : Exception
{
    public EResultType ResultType { get; }

    public ApiException(EResultType? resultType, string message) : base(message)
    {
        ResultType = resultType ?? EResultType.InternalServerError;
    }
}
