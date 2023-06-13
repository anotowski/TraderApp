using Mapster;
using TraderApp.Domain.Repositories;

namespace TraderApp.Application.Queries.Handlers;

public class GetStockByIdQueryHandler : IGetStockByIdQueryHandler
{
    private readonly IStockDetailsRepository _stockDetailsRepository;

    public GetStockByIdQueryHandler(IStockDetailsRepository stockDetailsRepository)
    {
        _stockDetailsRepository = stockDetailsRepository;
    }
    
    public async Task<IEnumerable<GetStockByIdResult>> HandleAsync(GetStockByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await _stockDetailsRepository.GetAsync(query.Id, cancellationToken);
        
        return result.Adapt<IEnumerable<GetStockByIdResult>>();
    }
}
