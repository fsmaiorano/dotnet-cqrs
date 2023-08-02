using Application.Common.Interfaces;
using Domain.Events.Post;
using MediatR;

namespace Application.UseCases.Post.Commands.DeletePost;

public record DeletePostCommand(int Id) : IRequest;

public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand>
{
    private readonly IBlogDataContext _context;
    public DeletePostCommandHandler(IBlogDataContext context)
    {
        _context = context;
    }

    public async Task Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Posts.FindAsync(request.Id) ??
                           throw new Exception("Post not found");

        _context.Posts.Remove(entity);

        entity.AddDomainEvent(new PostDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}
