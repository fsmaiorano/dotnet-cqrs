using Domain.Common;
using Domain.Entities;

namespace Domain.Events.Category;

public class CategoryCreatedEvent : BaseEvent
{
    public CategoryCreatedEvent(CategoryEntity category)
    {
        Category = category;
    }

    public CategoryEntity Category { get; set; }
}
