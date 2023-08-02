using Domain.Events.Post;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Post.EventHandlers;

public class PostCreatedEventHandler : INotificationHandler<PostCreatedEvent>
{
    private readonly ILogger<PostCreatedEventHandler> _logger;

    public PostCreatedEventHandler(ILogger<PostCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(PostCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Blog: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
