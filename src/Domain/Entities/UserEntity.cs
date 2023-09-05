using System.Diagnostics.CodeAnalysis;
using Domain.Common;

namespace Domain.Entities;

public class UserEntity : BaseAuditableEntity
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public string? Bio { get; set; }
    public string? Image { get; set; }
    public string? Slug { get; set; }
    public IList<PostEntity>? Posts { get; set; }
    public IList<RoleEntity>? Roles { get; set; }

    [SetsRequiredMembers]
    public UserEntity(string name, string email, string passwordHash, string bio = "", string image = "", string slug = "")
    {
        Email = email;
        PasswordHash = passwordHash;
        Name = name;
        Bio = bio;
        Image = image;

        if (string.IsNullOrEmpty(slug))
            Slug = ToSlug(name);
        else
            Slug = slug;
    }

    private string ToSlug(string title)
    {
        return title.ToLower().Replace(" ", "-");
    }
}
