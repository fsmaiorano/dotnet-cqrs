using Application.UseCases.User.Queries.GetUser;
using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IAuthService
{
    Task<string?> HandleUserAuthentication(UserAuthenticationDto user);
}
