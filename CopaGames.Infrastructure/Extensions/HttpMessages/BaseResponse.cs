using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Text.Json.Serialization;
using CopaGames.Domain.Enums.CommandResult;

namespace CopaGames.Infrastructure.Extensions.HttpMessages;

public class BaseResponse
{
    public string RequestId { get; set; }
    public string Message { get; set; }
    
    [JsonIgnore, NotMapped]
    public bool IsErrorResponse { get; set; }
    public string ErrorDetails { get; set; }
    public IEnumerable<string> ErrorList { get; set; }
    
    [JsonIgnore, NotMapped]
    public EResultType ResultType { get; set; }
}

public class BaseResponse<TData> : BaseResponse
{
    public virtual TData Data { get; set; }
}
