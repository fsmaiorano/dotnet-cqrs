using Domain.Common;
using Domain.Entities;

namespace Domain.Events.Post;

public class PostDeletedEvent : BaseEvent
{
    public PostDeletedEvent(PostEntity post)
    {
        Post = post;
    }

    public PostEntity Post { get; set; }
}
