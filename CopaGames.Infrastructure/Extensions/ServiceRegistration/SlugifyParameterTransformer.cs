using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Routing;

namespace CopaGames.Infrastructure.Extensions.ServiceRegistration;

public class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        return value?.ToString() is null ? null :
            Regex.Replace(value.ToString()!, "([a-z])([A-Z])", "$1-$2").ToLower();
    }
}