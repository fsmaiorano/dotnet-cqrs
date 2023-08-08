using Domain.Common;

namespace Domain.Entities;

public class UserEntity : BaseAuditableEntity
{
    public string? Name { get; set; }
    public required string Email { get; set; }
    public string? PasswordHash { get; set; }
    public string? Bio { get; set; }
    public string? Image { get; set; }
    public string? Slug { get; set; }
    public IList<PostEntity>? Posts { get; set; }
    public IList<RoleEntity>? Roles { get; set; }
}
