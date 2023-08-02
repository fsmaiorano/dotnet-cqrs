using Domain.Events.Category;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Category.EventHandlers;

public class CategoryDeletedEventHandler : INotificationHandler<CategoryDeletedEvent>
{
    private readonly ILogger<CategoryDeletedEventHandler> _logger;

    public CategoryDeletedEventHandler(ILogger<CategoryDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(CategoryDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Blog: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
