namespace Infrastructure.Mapping;

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CategoryMap : IEntityTypeConfiguration<CategoryEntity>
{
    public void Configure(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder.ToTable("Category");
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => p.Id);

        builder.Property(p => p.Id).HasColumnName("id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();
        builder.Property(p => p.Name).HasColumnName("name").HasColumnType("nvarchar(100)").IsRequired();
        builder.Property(p => p.Slug).HasColumnName("slug").HasColumnType("nvarchar(200)").IsRequired();

        builder.Property(p => p.Created).HasColumnName("created").HasColumnType("datetime").IsRequired();
        builder.Property(p => p.CreatedBy).HasColumnName("created_by").HasColumnType("nvarchar(100)").IsRequired();
        builder.Property(p => p.LastModified).HasColumnName("last_modified").HasColumnType("datetime").IsRequired(false);
        builder.Property(p => p.LastModifiedBy).HasColumnName("last_modified_by").HasColumnType("nvarchar(100)").IsRequired(false);
    }
}

