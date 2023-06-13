using TraderApp.Shared.Query;

namespace TraderApp.Application.Queries.Handlers;

public interface IGetStockByIdQueryHandler : IQueryHandler<GetStockByIdQuery, IEnumerable<GetStockByIdResult>>
{
}
