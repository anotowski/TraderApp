using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;
using TraderApp.Domain.Models.StockLog;
using TraderApp.Domain.Repositories;

namespace TraderApp.Infrastructure.AzureAdapters.TableStorage;

public class StockLogRepository : IStockLogRepository
{
    private readonly TableServiceClient _serviceClient;
    private readonly AzureStorageSettings _azureSettings;
    private readonly IConfiguration _configuration;

    public StockLogRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _azureSettings = new AzureStorageSettings();
        configuration.GetSection("AzureStorage").Bind(_azureSettings);
        _serviceClient = new TableServiceClient(_azureSettings.ConnectionString);
    }
    
    public StockLog[] GetWithinDateAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default)
    {
        var tableClient = _serviceClient.GetTableClient(_azureSettings.TableName);
        
        var results =  tableClient.Query<StockLogTableItem>(
            entity => entity.PartitionKey == _configuration["TradeSiteRedditEndpoint"] && entity.AttemptDate >= from && entity.AttemptDate <= to);
        
        return results.Select(stockLog => new StockLog()
            {
                Id = stockLog.BlobId != null ? Guid.Parse(stockLog.BlobId) : null,
                AttemptDate = stockLog.AttemptDate,
                AttemptResult = Enum.Parse<AttemptResult>(stockLog.AttemptResult),
                AttemptResultMessage = stockLog.AttemptResultMessage,
                EndpointName = stockLog.PartitionKey
            })
            .ToArray();
    }

    public async Task AddAsync(StockLog stockLog, CancellationToken cancellationToken = default)
    {
        var tableClient = _serviceClient.GetTableClient(_azureSettings.TableName);

        var stockLogTableItem = new StockLogTableItem()
        {
            BlobId = stockLog.Id.HasValue ? stockLog.Id.ToString() : null,
            PartitionKey = stockLog.EndpointName,
            AttemptDate = DateTime.UtcNow,
            RowKey = stockLog.AttemptDate.ToString("yyyyMMddHHmmss"),
            AttemptResult = stockLog.AttemptResult.ToString(),
            AttemptResultMessage = stockLog.AttemptResultMessage,
        };

        try
        {
            await tableClient.AddEntityAsync(stockLogTableItem, cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
