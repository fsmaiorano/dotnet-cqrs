using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.User.Queries.GetUser;

public record GetUserQuery : IRequest<List<UserDto>>;

public class GetUsersQueryHandler : IRequestHandler<GetUserQuery, List<UserDto>>
{
    private readonly IMapper _mapper;
    private readonly IBlogDataContext _context;

    public GetUsersQueryHandler(IBlogDataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        return await _context.Users.ProjectTo<UserDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        //return await _context.Users.ToListAsync(cancellationToken);
    }
}
