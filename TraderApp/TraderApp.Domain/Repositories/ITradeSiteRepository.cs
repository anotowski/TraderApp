using TraderApp.Domain.Models.StockDetails;

namespace TraderApp.Domain.Repositories;

public interface ITradeSiteRepository
{
    public Task<StockDetails[]> GetAsync(string endpointRoute, CancellationToken cancellationToken = default);
}


