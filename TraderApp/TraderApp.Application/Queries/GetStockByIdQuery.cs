using TraderApp.Shared.Query;

namespace TraderApp.Application.Queries;

public class GetStockByIdQuery : IQuery<IEnumerable<GetStockByIdResult>>
{
    private readonly Guid _id;

    public Guid Id
    {
        get => _id;
        init
        {
            if(value == Guid.Empty)
            {
                throw new ArgumentNullException( nameof(value));
            }
            
            _id = value;
        }
    }
}
