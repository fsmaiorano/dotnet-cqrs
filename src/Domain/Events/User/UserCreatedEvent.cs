using Domain.Common;
using Domain.Entities;

namespace Domain.Events.User;

public class UserCreatedEvent : BaseEvent
{
    public UserCreatedEvent(UserEntity user)
    {
        User = user;
    }

    public UserEntity User { get; set; }
}
