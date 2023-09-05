using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.User.Queries.GetUser;

public record GetAuthUserQuery : IRequest<UserAuthenticationDto?>
{
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
};

public class GetAuthUsersQueryHandler : IRequestHandler<GetAuthUserQuery, UserAuthenticationDto?>
{
    private readonly IMapper _mapper;
    private readonly IBlogDataContext _context;

    public GetAuthUsersQueryHandler(IBlogDataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserAuthenticationDto?> Handle(GetAuthUserQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.PasswordHash))
            return null;

        var storedUser = await _context.Users.Where(x => x.PasswordHash!.Equals(request.PasswordHash) && x.Email.Equals(request.Email))
                                             .ProjectTo<UserAuthenticationDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);

        if (storedUser is null || !request.PasswordHash.Equals(storedUser.PasswordHash))
            return null;

        return storedUser;
    }
}
