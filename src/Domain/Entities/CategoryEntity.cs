using Domain.Common;

namespace Domain.Entities;

public class CategoryEntity : BaseAuditableEntity
{
    public string? Name { get; set; }
    public string? Slug { get; set; }
    public List<PostEntity>? Posts { get; set; }
}
