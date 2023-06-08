using Microsoft.Extensions.DependencyInjection;
using TraderApp.Application.Commands.Handlers;

namespace TraderApp.Application.Container;

public static class ApplicationContainer
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        services.AddTransient<IUploadTradeSiteCommandHandler, UploadTradeSiteCommandHandler>();
        return services;
    }
}
