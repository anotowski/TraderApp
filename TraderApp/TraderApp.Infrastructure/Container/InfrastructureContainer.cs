using Azure.Data.Tables;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using TraderApp.Domain.Repositories;
using TraderApp.Infrastructure.AzureAdapters;
using TraderApp.Infrastructure.AzureAdapters.BlobStorage;
using TraderApp.Infrastructure.AzureAdapters.TableStorage;
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
        var azureSettings = new AzureStorageSettings();
        
            configuration.GetSection("AzureStorage").Bind(azureSettings);
        
        if (azureSettings == null)
        {
            throw new InvalidOperationException(nameof(azureSettings));
        }
        
        services.AddAzureClients(clientBuilder =>
        {
            clientBuilder.AddBlobServiceClient(azureSettings.ConnectionString);
        });
        
        services.AddScoped(_ => new TableServiceClient(azureSettings.ConnectionString));
        services.AddTransient<ITradeSiteRepository, TradeSiteRepository>();
        services.AddTransient<IStockLogRepository, StockLogRepository>();
        services.AddTransient<IStockDetailsRepository, StockDetailsRepository>();
        
        return services;
    }
}
