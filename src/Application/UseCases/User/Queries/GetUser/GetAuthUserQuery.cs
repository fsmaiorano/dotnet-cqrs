using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.User.Queries.GetUser;

public record GetAuthUserQuery : IRequest<UserDto?>
{
    public string? Email { get; set; }
    public string? Password { get; set; }
};

public class GetAuthUsersQueryHandler : IRequestHandler<GetAuthUserQuery, UserDto?>
{
    private readonly IMapper _mapper;
    private readonly IBlogDataContext _context;

    public GetAuthUsersQueryHandler(IBlogDataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserDto?> Handle(GetAuthUserQuery request, CancellationToken cancellationToken)
    {
        return await _context.Users.ProjectTo<UserDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
    }
}
