using Microsoft.Extensions.DependencyInjection;
using TraderApp.Application.Commands.Handlers;
using TraderApp.Application.Parsers;
using TraderApp.Application.Queries.Handlers;

namespace TraderApp.Application.Container;

public static class ApplicationContainer
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        services.AddTransient<IUploadTradeSiteCommandHandler, UploadTradeSiteCommandHandler>();
        services.AddTransient<IGetStockByIdQueryHandler, GetStockByIdQueryHandler>();
        services.AddTransient<IGetStockLogQueryHandler, GetStockLogQueryHandler>();
        services.AddTransient<IDateTimeParser, DateTimeParser>();
        
        return services;
    }
}
