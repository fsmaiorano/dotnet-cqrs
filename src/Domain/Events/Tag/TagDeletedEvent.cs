using Domain.Common;
using Domain.Entities;

namespace Domain.Events.Tag;

public class TagDeletedEvent : BaseEvent
{
    public TagDeletedEvent(TagEntity tag)
    {
        Tag = tag;
    }

    public TagEntity Tag { get; set; }
}
