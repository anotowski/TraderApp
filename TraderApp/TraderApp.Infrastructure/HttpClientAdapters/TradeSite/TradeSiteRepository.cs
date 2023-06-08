using System.Text.Json;
using Microsoft.Extensions.Configuration;
using TraderApp.Domain.Models.StockDetails;
using TraderApp.Domain.Repositories;

namespace TraderApp.Infrastructure.HttpClientAdapters.TradeSite;

public class TradeSiteRepository : ITradeSiteRepository
{
    private readonly HttpClient _httpClient;

    public TradeSiteRepository(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient(ConstValues.TradeSiteHttpClientName);
    }
    
    public async Task<StockDetails[]> GetAsync(string endpointRoute, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync(endpointRoute, cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            var traderSiteResponses = JsonSerializer.Deserialize<TradeSiteResponse[]>(content);
            
            return traderSiteResponses.Select(traderSiteResponse => new StockDetails
            {
                NumberOfComments = traderSiteResponse.no_of_comments,
                Sentiment = traderSiteResponse.sentiment,
                SentimentScore = traderSiteResponse.sentiment_score,
                Ticker = traderSiteResponse.ticker
            }).ToArray();
        }
        
        throw new InvalidOperationException(
            $"Could not get data from external api: {endpointRoute}. Response statusCode: {response.StatusCode}");
    }
}
