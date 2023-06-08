using TraderApp.Domain.Models.StockDetails;

namespace TraderApp.Domain.Repositories;

public interface IStockDetailsRepository
{
    Task<StockDetails> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Guid> AddAsync(StockDetails[] stockDetails, CancellationToken cancellationToken = default);
}
