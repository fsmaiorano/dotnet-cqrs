using Domain.Common;
using Domain.Entities;

namespace Domain.Events.Tag;

public class TagCreatedEvent : BaseEvent
{
    public TagCreatedEvent(TagEntity tag)
    {
        Tag = tag;
    }

    public TagEntity Tag { get; set; }
}
