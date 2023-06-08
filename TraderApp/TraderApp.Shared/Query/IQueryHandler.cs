namespace TraderApp.Shared.Query;

public interface IQueryHandler<in TQuery, TResult> where TQuery : class, IQuery<TResult>
{
    public Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken);
}
