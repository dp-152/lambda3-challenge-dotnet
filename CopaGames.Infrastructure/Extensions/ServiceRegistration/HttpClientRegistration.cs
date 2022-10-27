using CopaGames.Domain.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CopaGames.Infrastructure.Extensions.ServiceRegistration;

public static class HttpClientRegistration
{
    public static void RegisterHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        var externalApis = configuration.GetSection("ExternalAPIs").Get<Dictionary<string, ExternalApiConfigData>>();
        foreach (var (apiName, apiData) in externalApis.Select(x => (x.Key, x.Value)))
        {
            services.AddHttpClient(apiName, client =>
            {
                client.BaseAddress = new Uri(apiData.BaseUrl);
            });
        }
    }
}
