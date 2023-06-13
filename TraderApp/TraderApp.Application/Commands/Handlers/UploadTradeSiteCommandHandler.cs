using TraderApp.Domain.Models.StockDetails;
using TraderApp.Domain.Models.StockLog;
using TraderApp.Domain.Repositories;

namespace TraderApp.Application.Commands.Handlers;

public class UploadTradeSiteCommandHandler : IUploadTradeSiteCommandHandler
{
    private readonly ITradeSiteRepository _tradeSiteRepository;
    private readonly IStockLogRepository _stockLogRepository;
    private readonly IStockDetailsRepository _stockDetailsRepository;

    public UploadTradeSiteCommandHandler(
        ITradeSiteRepository tradeSiteRepository,
        IStockLogRepository stockLogRepository,
        IStockDetailsRepository stockDetailsRepository)
    {
        _tradeSiteRepository = tradeSiteRepository;
        _stockLogRepository = stockLogRepository;
        _stockDetailsRepository = stockDetailsRepository;
    }

    public async Task<Guid> HandleAsync(UploadTradeSiteCommand command, CancellationToken cancellationToken = default)
    {
        var stockDetails = new List<StockDetails>();
        try
        {
            stockDetails = (await _tradeSiteRepository.GetAsync(command.EndpointRoute, cancellationToken)).ToList();
        }
        catch (Exception e)
        {
            var errorLog = new StockLog
            {
                AttemptDate = DateTime.UtcNow,
                AttemptResult = AttemptResult.Failure,
                EndpointName = command.EndpointRoute,
                AttemptResultMessage = e.Message
            };

            await _stockLogRepository.AddAsync(errorLog, cancellationToken);

            throw;
        }

        var id = await _stockDetailsRepository.AddAsync(stockDetails, cancellationToken);
        var stockLog = new StockLog
        {
            Id = id,
            AttemptDate = DateTime.UtcNow,
            AttemptResult = AttemptResult.Success,
            EndpointName = command.EndpointRoute,
            AttemptResultMessage = "Success"
        };

        await _stockLogRepository.AddAsync(stockLog, cancellationToken);

        return id;
    }
}
