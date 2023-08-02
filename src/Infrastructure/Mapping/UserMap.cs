namespace Infrastructure.Mapping;

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserMap : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("User");
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => p.Id);

        builder.Property(p => p.Id).HasColumnName("id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn(); ;
        builder.Property(p => p.Name).HasColumnName("name").HasColumnType("nvarchar(100)").IsRequired();
        builder.Property(p => p.Slug).HasColumnName("slug").HasColumnType("nvarchar(200)").IsRequired();
        builder.Property(p => p.Email).HasColumnName("email").HasColumnType("nvarchar(200)").IsRequired();
        builder.Property(p => p.PasswordHash).HasColumnName("password_hash").HasColumnType("nvarchar(200)").IsRequired();
        builder.Property(p => p.Bio).HasColumnName("bio").HasColumnType("nvarchar(250)").IsRequired();
        builder.Property(p => p.Image).HasColumnName("image").HasColumnType("nvarchar(250)").IsRequired();


        builder.HasMany(p => p.Roles).WithMany(p => p.Users).UsingEntity<Dictionary<string, object>>("UserRole",
                                                           role => role.HasOne<RoleEntity>()
                                                                       .WithMany()
                                                                       .HasForeignKey("RoleId")
                                                                       .HasConstraintName("FK_UserRole_RoleId")
                                                                       .OnDelete(DeleteBehavior.Cascade),

                                                             user => user.HasOne<UserEntity>()
                                                                       .WithMany()
                                                                       .HasForeignKey("UserId")
                                                                       .HasConstraintName("FK_UserRole_UserId")
                                                                       .OnDelete(DeleteBehavior.Cascade));

        builder.Property(p => p.Created).HasColumnName("created").HasColumnType("datetime").IsRequired();
        builder.Property(p => p.CreatedBy).HasColumnName("created_by").HasColumnType("nvarchar(100)").IsRequired();
        builder.Property(p => p.LastModified).HasColumnName("last_modified").HasColumnType("datetime").IsRequired(false);
        builder.Property(p => p.LastModifiedBy).HasColumnName("last_modified_by").HasColumnType("nvarchar(100)").IsRequired(false);
    }
}

