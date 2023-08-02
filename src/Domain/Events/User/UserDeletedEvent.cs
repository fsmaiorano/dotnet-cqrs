using Domain.Common;
using Domain.Entities;

namespace Domain.Events.User;

public class UserDeletedvent : BaseEvent
{
    public UserDeletedvent(UserEntity user)
    {
        User = user;
    }

    public UserEntity User { get; set; }
}
