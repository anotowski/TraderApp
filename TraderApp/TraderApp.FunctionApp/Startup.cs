using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TraderApp.Application.Container;
using TraderApp.Infrastructure.Container;

[assembly: FunctionsStartup(typeof(TraderApp.FunctionApp.Startup))]

namespace TraderApp.FunctionApp;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var configuration = BuildConfiguration(builder.GetContext().ApplicationRootPath);
        builder.Services.Configure<IConfiguration>(configuration);
        builder.Services.AddSingleton<IConfiguration>(configuration);
        builder.Services.RegisterInfrastructure(configuration);
        builder.Services.RegisterApplication();

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
