using Domain.Common;
using Domain.Entities;

namespace Domain.Events.Post;

public class PostCreatedEvent : BaseEvent
{
    public PostCreatedEvent(PostEntity post)
    {
        Post = post;
    }

    public PostEntity Post { get; set; }
}
