using TraderApp.Domain.Models.StockLog;

namespace TraderApp.Domain.Repositories;

public interface IStockLogRepository
{
    StockLog[] GetWithinDateAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default);
    Task AddAsync(StockLog stockLog, CancellationToken cancellationToken = default);
}
