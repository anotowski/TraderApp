using Azure;
using Azure.Data.Tables;

namespace TraderApp.Infrastructure.AzureAdapters.TableStorage;

public class StockLogTableItem : ITableEntity
{
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
    public DateTime AttemptDate { get; set; }
    public string? BlobId { get; set; }
    public string AttemptResult { get; set; }
    public string AttemptResultMessage { get; set; }
}
