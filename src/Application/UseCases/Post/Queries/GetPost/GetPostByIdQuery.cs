using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Post.Queries.GetPost;

public record GetPostByIdQuery : IRequest<PostEntity?>
{
    public int Id { get; set; }
};

public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, PostEntity?>
{
    private readonly IBlogDataContext _context;

    public GetPostByIdQueryHandler(IBlogDataContext context)
    {
        _context = context;
    }

    public async Task<PostEntity?> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Posts.Where(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
    }
}
