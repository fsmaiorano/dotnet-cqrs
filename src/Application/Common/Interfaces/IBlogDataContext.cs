using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IBlogDataContext
{
    DbSet<UserEntity> Users { get; }
    DbSet<PostEntity> Posts { get; }
    DbSet<TagEntity> Tags { get; }
    DbSet<CategoryEntity> Categories { get; }
    DbSet<RoleEntity> Roles { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
