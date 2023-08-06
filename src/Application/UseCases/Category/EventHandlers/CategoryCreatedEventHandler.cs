using Domain.Events.Category;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Category.EventHandlers;

public class CategoryCreatedEventHandler : INotificationHandler<CategoryCreatedEvent>
{
    private readonly ILogger<CategoryCreatedEventHandler> _logger;

    public CategoryCreatedEventHandler(ILogger<CategoryCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(CategoryCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Blog: {DomainEvent}", notification.GetType().Name);
        _logger.LogDebug("Blog: {@DomainEvent}", notification);

        return Task.CompletedTask;
    }
}
