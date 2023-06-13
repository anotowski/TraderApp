namespace TraderApp.Application.Queries;

public class GetStockByIdResult
{
    public int NumberOfComments { get; init; }
    public string Sentiment { get; init; }
    public double SentimentScore { get; init; }
    public string Ticker { get; init; }
}
