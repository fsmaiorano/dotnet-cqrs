using Domain.Common;

namespace Domain.Entities;

public class PostEntity : BaseAuditableEntity
{
    public int CategoryId { get; set; }
    public virtual CategoryEntity? Category { get; set; }
    public int AuthorId { get; set; }
    public virtual UserEntity? Author { get; set; }
    public string? Title { get; set; }
    public string? Summary { get; set; }
    public string? Body { get; set; }
    public string? Slug { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public List<TagEntity>? Tags { get; set; }
}
