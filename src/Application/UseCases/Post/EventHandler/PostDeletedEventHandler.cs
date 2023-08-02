using Domain.Events.Post;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Post.EventHandlers;

public class PostDeletedEventHandler : INotificationHandler<PostDeletedEvent>
{
    private readonly ILogger<PostDeletedEventHandler> _logger;

    public PostDeletedEventHandler(ILogger<PostDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(PostDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Blog: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
