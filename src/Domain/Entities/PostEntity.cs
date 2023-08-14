using Domain.Common;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities;

public class PostEntity : BaseAuditableEntity
{
    public required int CategoryId { get; set; }
    public virtual CategoryEntity? Category { get; set; }
    public required int AuthorId { get; set; }
    public virtual UserEntity? Author { get; set; }
    public required string Title { get; set; }
    public string? Summary { get; set; }
    public required string Body { get; set; }
    public string? Slug { get; set; }
    public bool IsPublished { get; private set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; private set; }
    public List<TagEntity>? Tags { get; set; }

    [SetsRequiredMembers]
    public PostEntity(string title, string body, int categoryId, int authorId, string slug = "")
    {
        Title = title;
        Body = body;
        CategoryId = categoryId;
        AuthorId = authorId;
        CreateDate = DateTime.UtcNow;

        IsPublished = false;

        if (string.IsNullOrEmpty(slug))
            Slug = ToSlug(title);
        else
            Slug = slug;
    }

    public void Publish()
    {
        IsPublished = true;
        Touch();
    }

    private void Touch()
    {
        UpdateDate = DateTime.UtcNow;
    }

    private string ToSlug(string title)
    {
        return title.ToLower().Replace(" ", "-");
    }
}
