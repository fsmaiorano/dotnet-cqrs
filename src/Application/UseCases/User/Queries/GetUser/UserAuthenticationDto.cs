using System.Text.Json.Serialization;
using Application.Common.Mapping;
using Domain.Entities;

namespace Application.UseCases.User.Queries.GetUser
{
    public class UserAuthenticationDto : IMapFrom<UserEntity>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
    }
}
