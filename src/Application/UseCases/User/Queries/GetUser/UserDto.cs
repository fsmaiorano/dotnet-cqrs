using Application.Common.Mapping;
using Domain.Entities;

namespace Application.UseCases.User.Queries.GetUser;
public class UserDto : IMapFrom<UserEntity>
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Bio { get; set; }
    public string? Image { get; set; }
    public string? Slug { get; set; }
}
