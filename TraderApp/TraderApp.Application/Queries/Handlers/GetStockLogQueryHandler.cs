using Mapster;
using TraderApp.Application.Parsers;
using TraderApp.Domain.Repositories;

namespace TraderApp.Application.Queries.Handlers;

public class GetStockLogQueryHandler : IGetStockLogQueryHandler
{
    private readonly IStockLogRepository _stockLogRepository;
    private readonly IDateTimeParser _dateTimeParser;

    public GetStockLogQueryHandler(IStockLogRepository stockLogRepository,
        IDateTimeParser dateTimeParser)
    {
        _stockLogRepository = stockLogRepository;
        _dateTimeParser = dateTimeParser;
    }
    
    public Task<IEnumerable<GetStockLogResult>> HandleAsync(GetStockLogQuery query, CancellationToken cancellationToken)
    {
        var fromDate = _dateTimeParser.Parse(query.From);
        var toDate = _dateTimeParser.Parse(query.To);
        
        var result = _stockLogRepository.GetWithinDateAsync(
            fromDate, 
            toDate, 
            cancellationToken);

        return Task.FromResult(result.Adapt<IEnumerable<GetStockLogResult>>());
    }
}
