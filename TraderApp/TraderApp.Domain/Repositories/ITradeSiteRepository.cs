using TraderApp.Domain.Models.StockDetails;

namespace TraderApp.Domain.Repositories;

public interface ITradeSiteRepository
{
    public Task<IEnumerable<StockDetails>> GetAsync(string endpointRoute, CancellationToken cancellationToken = default);
}


