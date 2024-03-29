namespace TraderApp.Domain.Models.StockLog;

public class StockLog
{
    public Guid? Id { get; init; }
    public string EndpointName { get; init; }
    public AttemptResult AttemptResult { get; init; }
    public DateTime AttemptDate { get; init; }
    public string AttemptResultMessage { get; init; }
}
