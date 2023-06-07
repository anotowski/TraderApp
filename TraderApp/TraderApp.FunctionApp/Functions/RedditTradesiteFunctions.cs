using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace TraderApp.FunctionApp.Functions;

public class RedditTradeSiteFunctions
{
    
    [FunctionName("UploadRedisTradeSiteDataFunction")]
    public async Task Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer, ILogger log)
    {
        log.LogInformation(
            $"{nameof(RedditTradeSiteFunctions)} function upload redis trade site data started at: {DateTime.Now}");
    }
}
