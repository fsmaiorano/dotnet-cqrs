using Domain.Events.Tag;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Tag.EventHandlers;

public class TagCreatedEventHandler : INotificationHandler<TagCreatedEvent>
{
    private readonly ILogger<TagCreatedEventHandler> _logger;

    public TagCreatedEventHandler(ILogger<TagCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(TagCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Blog: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
