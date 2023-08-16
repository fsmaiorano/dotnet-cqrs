using System.Reflection;
using Application.Common.Security;
using MediatR;

namespace Application;

public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public AuthorizationBehaviour()
    {

    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var authAttribute = request.GetType().GetCustomAttribute<AuthorizeAttribute>();
        throw new NotImplementedException();
    }
}
