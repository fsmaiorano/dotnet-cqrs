using Domain.Common;
using Domain.Entities;

namespace Domain.Events.Category;

public class CategoryDeletedEvent : BaseEvent
{
    public CategoryDeletedEvent(CategoryEntity category)
    {
        Category = category;
    }

    public CategoryEntity Category { get; set; }
}
