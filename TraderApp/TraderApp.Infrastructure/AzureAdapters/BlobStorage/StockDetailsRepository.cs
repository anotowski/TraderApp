using System.Text;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TraderApp.Domain.Models.StockDetails;
using TraderApp.Domain.Repositories;

namespace TraderApp.Infrastructure.AzureAdapters.BlobStorage;

public class StockDetailsRepository : IStockDetailsRepository
{
    private readonly BlobContainerClient _containerClient;
    
    public StockDetailsRepository(IConfiguration configuration)
    {
        var azureSettings = new AzureStorageSettings();
        configuration.GetSection("AzureStorage").Bind(azureSettings);
        
        _containerClient = new BlobContainerClient(azureSettings.ConnectionString, azureSettings.ContainerName);
    }
    
    public async Task<StockDetails[]> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var blobClient = _containerClient.GetBlobClient(id.ToString());

        using var memoryStream = new MemoryStream();
        
        try
        {
            await blobClient.DownloadToAsync(memoryStream, cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
            
        memoryStream.Position = 0;

        using var streamReader = new StreamReader(memoryStream, Encoding.UTF8);
        var jsonData = await streamReader.ReadToEndAsync();
        var result = JsonConvert.DeserializeObject<IEnumerable<StockDetails>>(jsonData);

        return result.ToArray();
    }

    public async Task<Guid> AddAsync(IEnumerable<StockDetails> stockDetails, CancellationToken cancellationToken = default)
    {
        var json = JsonConvert.SerializeObject(stockDetails);
        var id = Guid.NewGuid();
        var blob = _containerClient.GetBlobClient(id.ToString());

        using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(json));
        
        await blob.UploadAsync(memoryStream, cancellationToken);

        return id;
    }
}
