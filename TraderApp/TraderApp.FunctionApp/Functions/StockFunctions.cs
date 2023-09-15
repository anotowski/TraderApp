using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TraderApp.Application.Queries;
using TraderApp.Application.Queries.Handlers;
using System;

namespace TraderApp.FunctionApp.Functions
{
    public class StockFunctions
    {
        private readonly IGetStockLogQueryHandler _getStockLogQueryHandler;
        private readonly IGetStockByIdQueryHandler _getStockByIdQueryHandler;

        public StockFunctions(
            IGetStockLogQueryHandler getStockLogQueryHandler,
            IGetStockByIdQueryHandler getStockByIdQueryHandler)
        {
            _getStockLogQueryHandler = getStockLogQueryHandler;
            _getStockByIdQueryHandler = getStockByIdQueryHandler;
        }

        [FunctionName("stockLogs")]
        public async Task<IActionResult> GetStockLogs(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest request,
            ILogger log)
        {
            log.LogInformation("Starting to get stock logs.");
            string from = request.Query["from"];
            string to = request.Query["to"];
            var query = new GetStockLogQuery
            {
                From = from,
                To = to,
            };

            var response = await _getStockLogQueryHandler
                .HandleAsync(query, request.HttpContext.RequestAborted);
            log.LogInformation("Get stock logs finished.");

            return new OkObjectResult(response);
        }


        [FunctionName("stock")]
        public async Task<IActionResult> GetStock(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "stock/{id:guid}")] HttpRequest request,
            Guid id,
            ILogger log)
        {
            log.LogInformation("Starting to get stock details.");
            var query = new GetStockByIdQuery { Id = id };

            var response = await _getStockByIdQueryHandler
                .HandleAsync(query, request.HttpContext.RequestAborted);
            log.LogInformation("Get stock logs finished.");

            return new OkObjectResult(response);
        }
    }
}
