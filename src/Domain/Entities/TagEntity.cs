﻿using System.Diagnostics.CodeAnalysis;
using Domain.Common;

namespace Domain.Entities;

public class TagEntity : BaseAuditableEntity
{
    public required string Name { get; set; }
    public string? Slug { get; set; }
    public IList<PostEntity>? Posts { get; set; }

    [SetsRequiredMembers]
    public TagEntity(string name, string slug = "")
    {
        Name = name;

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
