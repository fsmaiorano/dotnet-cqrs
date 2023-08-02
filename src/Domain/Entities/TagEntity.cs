using Domain.Common;

namespace Domain.Entities;

public class TagEntity : BaseAuditableEntity
{
    public string? Name { get; set; }
    public string? Slug { get; set; }
    public IList<PostEntity>? Posts { get; set; }
}
