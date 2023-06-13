using TraderApp.Shared.Query;

namespace TraderApp.Application.Queries;

public class GetStockLogQuery : IQuery<IEnumerable<GetStockLogResult>>
{
    public string From { get; init; }
    public string To { get; init; }
}
