using TraderApp.Shared.Command;

namespace TraderApp.Application.Commands;

public class UploadTradeSiteCommand : ICommand
{
    public Guid Id { get; set; }
    public string EndpointRoute { get; init; }
}
