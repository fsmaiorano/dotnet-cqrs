using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Post.Commands.UpdatePost;

public record PublishPostCommand : IRequest
{
    public int Id { get; set; }
    public bool IsPublished { get; set; }
}

public class PublishPostCommandHandler : IRequestHandler<PublishPostCommand>
{
    private readonly IBlogDataContext _context;

    public PublishPostCommandHandler(IBlogDataContext context)
    {
        _context = context;
    }

    public async Task Handle(PublishPostCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Posts.FindAsync(new object[] { request.Id, }, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(UserEntity), request.Id);

        entity.Publish();

        await _context.SaveChangesAsync(cancellationToken);
    }
}
