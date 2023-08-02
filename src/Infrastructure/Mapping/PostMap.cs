namespace Infrastructure.Mapping;

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PostMap : IEntityTypeConfiguration<PostEntity>
{
    public void Configure(EntityTypeBuilder<PostEntity> builder)
    {
        builder.ToTable("Post");
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => p.Id);

        builder.Property(p => p.Id).HasColumnName("id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn(); ;
        builder.Property(p => p.CategoryId).HasColumnName("category_id").HasColumnType("int").IsRequired();
        builder.Property(p => p.AuthorId).HasColumnName("author_id").HasColumnType("int").IsRequired();
        builder.Property(p => p.Title).HasColumnName("title").HasColumnType("nvarchar(200)").IsRequired();
        builder.Property(p => p.Summary).HasColumnName("summary").HasColumnType("nvarchar(500)").IsRequired();
        builder.Property(p => p.Slug).HasColumnName("slug").HasColumnType("nvarchar(200)").IsRequired();
        builder.Property(p => p.Body).HasColumnName("body").HasColumnType("nvarchar(max)").IsRequired();
        builder.Property(p => p.CreateDate).HasColumnName("create_date").HasColumnType("datetime").IsRequired().HasDefaultValue(DateTime.Now.ToUniversalTime());
        builder.Property(p => p.UpdateDate).HasColumnName("update_date").HasColumnType("datetime").IsRequired(false);

        builder.HasOne(p => p.Author).WithMany(p => p.Posts).HasForeignKey(p => p.AuthorId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(p => p.Category).WithMany(p => p.Posts).HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Tags).WithMany(p => p.Posts).UsingEntity<Dictionary<string, object>>("PostTag",
                                                            post => post.HasOne<TagEntity>()
                                                                        .WithMany()
                                                                        .HasForeignKey("PostId")
                                                                        .HasConstraintName("FK_PostTag_PostId")
                                                                        .OnDelete(DeleteBehavior.Cascade),

                                                              tag => tag.HasOne<PostEntity>()
                                                                        .WithMany()
                                                                        .HasForeignKey("TagId")
                                                                        .HasConstraintName("FK_PostTag_TagId")
                                                                        .OnDelete(DeleteBehavior.Cascade));

        builder.Property(p => p.Created).HasColumnName("created").HasColumnType("datetime").IsRequired();
        builder.Property(p => p.CreatedBy).HasColumnName("created_by").HasColumnType("nvarchar(100)").IsRequired();
        builder.Property(p => p.LastModified).HasColumnName("last_modified").HasColumnType("datetime").IsRequired(false);
        builder.Property(p => p.LastModifiedBy).HasColumnName("last_modified_by").HasColumnType("nvarchar(100)").IsRequired(false);
    }
}

