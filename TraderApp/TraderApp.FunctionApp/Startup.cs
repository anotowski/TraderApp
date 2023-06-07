using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TraderApp.FunctionApp.Helpers;

[assembly: FunctionsStartup(typeof(TraderApp.FunctionApp.Startup))]

namespace TraderApp.FunctionApp;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var configuration = BuildConfiguration(builder.GetContext().ApplicationRootPath);
        builder.Services.Configure<IConfiguration>(configuration);
        var tradeSiteApiBaseUrl = configuration["TradeSiteApiUrl"];
        builder.Services.AddHttpClient(ConstValues.TraderFunctionAppHttpClientName, c =>
        {
            c.BaseAddress = new Uri(tradeSiteApiBaseUrl);
        });

        builder.Services.AddLogging();
    }
    
    private IConfiguration BuildConfiguration(string applicationRootPath)
    {
        var config =
            new ConfigurationBuilder()
                .SetBasePath(applicationRootPath)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

        return config;
    }
}
