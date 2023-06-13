using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TraderApp.Application.Commands;
using TraderApp.Application.Commands.Handlers;

namespace TraderApp.FunctionApp.Functions;

public class RedditTradeSiteFunctions
{
    private readonly IUploadTradeSiteCommandHandler _uploadTradeSiteCommandHandler;
    private readonly IConfiguration _configuration;

    public RedditTradeSiteFunctions(
        IUploadTradeSiteCommandHandler uploadTradeSiteCommandHandler,
        IConfiguration configuration)
    {
        _uploadTradeSiteCommandHandler = uploadTradeSiteCommandHandler;
        _configuration = configuration;
    }

    /// <summary>
    /// Function gets data from external site and uploads it to azure blob storage as well as logs information in table storage
    /// </summary>
    /// <param name="myTimer">Timer schedule that will trigger function</param>
    /// <param name="log">Logger to log any information.</param>
    [FunctionName("UploadRedisTradeSiteDataFunction")]
    public async Task Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer, ILogger log)
    {
        log.LogInformation(
            $"{nameof(RedditTradeSiteFunctions)} function upload redis trade site data started at: {DateTime.Now}");
        var configValue = _configuration["TradeSiteRedditEndpoint"];
        await _uploadTradeSiteCommandHandler.HandleAsync(new UploadTradeSiteCommand { EndpointRoute = configValue });
        log.LogInformation($"Upload redis trade site data finished at: {DateTime.Now}.");
    }
}
