using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Post.Commands.UpdatePost;

public record UpdatePostCommand : IRequest
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public int AuthorId { get; set; }
    public string? Title { get; set; }
    public string? Summary { get; set; }
    public string? Body { get; set; }
    public string? Slug { get; set; }
}

public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand>
{
    private readonly IBlogDataContext _context;

    public UpdatePostCommandHandler(IBlogDataContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Posts.FindAsync(new object[] { request.Id, }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(UserEntity), request.Id);
        }

        entity.Slug = request.Slug ?? entity.Slug;
        entity.Title = request.Title ?? entity.Title;
        entity.Summary = request.Summary ?? entity.Summary;
        entity.Body = request.Body ?? entity.Body;
        entity.CategoryId = request.CategoryId > 0 ? request.CategoryId : entity.CategoryId;
        entity.AuthorId = request.AuthorId > 0 ? request.AuthorId : entity.AuthorId;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
