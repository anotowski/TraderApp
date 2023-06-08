namespace TraderApp.Domain.Models.StockDetails;

public class StockDetails
{
    public int NumberOfComments { get; set; }
    public string Sentiment { get; set; }
    public double SentimentScore { get; set; }
    public string Ticker { get; set; }
}
