using TraderApp.Shared.Command;

namespace TraderApp.Application.Commands;

public class UploadTradeSiteCommand : ICommand
{
    private readonly string _endpointRoute;
    
    public Guid Id { get; init; }

    public string EndpointRoute
    {
        get => _endpointRoute;
        init
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(EndpointRoute));
            }

            _endpointRoute = value;
        }
    }
}
