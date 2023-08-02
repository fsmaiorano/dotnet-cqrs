using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Post.Queries.GetPost;

public record GetPostQuery : IRequest<List<PostEntity>>;

public class GetPostQueryHandler : IRequestHandler<GetPostQuery, List<PostEntity>>
{
    private readonly IBlogDataContext _context;

    public GetPostQueryHandler(IBlogDataContext context)
    {
        _context = context;
    }

    public async Task<List<PostEntity>> Handle(GetPostQuery request, CancellationToken cancellationToken)
    {
        return await _context.Posts.ToListAsync(cancellationToken);
    }
}
