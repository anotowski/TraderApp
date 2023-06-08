using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using TraderApp.Domain.Repositories;
using TraderApp.Infrastructure.HttpClientAdapters.TradeSite;

namespace TraderApp.Infrastructure.Container;

public static class InfrastructureContainer
{
    public static IServiceCollection RegisterInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var tradeSiteApiBaseUrl = configuration["TradeSiteApiUrl"];

        if (tradeSiteApiBaseUrl == null)
        {
            throw new InvalidOperationException(nameof(tradeSiteApiBaseUrl));
        }

        services.AddHttpClient(ConstValues.TradeSiteHttpClientName, c =>
        {
            c.BaseAddress = new Uri(tradeSiteApiBaseUrl);
        });


        services.AddTransient<ITradeSiteRepository, TradeSiteRepository>();
        
        return services;
    }
}
