using TraderApp.Domain.Models.StockLog;

namespace TraderApp.Application.Queries;

public class GetStockLogResult
{
    public Guid? Id { get; init; }
    public string EndpointName { get; init; }
    public AttemptResult AttemptResult { get; init; }
    public DateTime AttemptDate { get; init; }
    public string AttemptResultMessage { get; init; }
}
