namespace TraderApp.Infrastructure.HttpClientAdapters.TradeSite;

public class TradeSiteResponse
{
    public int no_of_comments { get; set; }
    public string sentiment { get; set; }
    public double sentiment_score { get; set; }
    public string ticker { get; set; }
}
