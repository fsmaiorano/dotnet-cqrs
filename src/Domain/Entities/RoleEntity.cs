using Domain.Common;

namespace Domain.Entities;

public class RoleEntity : BaseAuditableEntity
{
    public string? Name { get; set; }
    public string? Slug { get; set; }
    public IList<UserEntity>? Users { get; set; }
}
