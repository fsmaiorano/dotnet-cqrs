using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IAuthService
{
    Task<string?> HandleUserAuthentication(UserEntity user);
}
