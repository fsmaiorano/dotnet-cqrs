using Domain.Events.Tag;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Tag.EventHandlers;

public class TagDeletedEventHandler : INotificationHandler<TagDeletedEvent>
{
    private readonly ILogger<TagDeletedEventHandler> _logger;

    public TagDeletedEventHandler(ILogger<TagDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(TagDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Blog: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
