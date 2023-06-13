using TraderApp.Shared.Query;

namespace TraderApp.Application.Queries.Handlers;

public interface IGetStockLogQueryHandler : IQueryHandler<GetStockLogQuery, IEnumerable<GetStockLogResult>>
{
}
