using System.ComponentModel;
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
        if (string.IsNullOrWhiteSpace(command.EndpointRoute))
        {
            throw new InvalidEnumArgumentException(nameof(command.EndpointRoute));
        }

        try
        {
            var tradeSiteResult = await _tradeSiteRepository.GetAsync(command.EndpointRoute, cancellationToken);
            var stockLog = new StockLog();
            await _stockLogRepository.AddAsync(stockLog, cancellationToken);
            return await _stockDetailsRepository.AddAsync(tradeSiteResult, cancellationToken);
        }
        catch (Exception e)
        {
            var stockLog = new StockLog();
            await _stockLogRepository.AddAsync(stockLog, cancellationToken);
            throw;
        }
    }
}
