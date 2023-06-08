namespace TraderApp.Shared.Command;

public interface ICommandHandler<in TCommand> where TCommand : class, ICommand
{
    Task<Guid> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}
