using Application.Common.Interfaces;
using Application.Common.Mapping;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.UseCases.User.Queries.GetUser;

public record GetUserWithPaginationQuery : IRequest<PaginatedList<UserDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;

};

public class GetUsersWithPaginationHandler : IRequestHandler<GetUserWithPaginationQuery, PaginatedList<UserDto>>
{
    private readonly IMapper _mapper;
    private readonly IBlogDataContext _context;

    public GetUsersWithPaginationHandler(IBlogDataContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<PaginatedList<UserDto>> Handle(GetUserWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Users.ProjectTo<UserDto>(_mapper.ConfigurationProvider).PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
