using Application.Common.Interfaces;
using Application.Common.Mapping;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Post.Queries.GetPost;

public record GetPostWithPaginationQuery : IRequest<PaginatedList<PostEntity>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;

};

public class GetPostWithPaginationHandler : IRequestHandler<GetPostWithPaginationQuery, PaginatedList<PostEntity>>
{
    private readonly IMapper _mapper;
    private readonly IBlogDataContext _context;

    public GetPostWithPaginationHandler(IBlogDataContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<PaginatedList<PostEntity>> Handle(GetPostWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Posts
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
