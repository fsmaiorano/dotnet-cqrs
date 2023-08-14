using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Post.Commands.UpdatePost;

public record PublishPostCommand : IRequest
{
    public required int Id { get; set; }
    public required int AuthorId { get; set; }
    public required bool IsPublished { get; set; }
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
        var entity = await _context.Posts.Where(x => x.Id == request.Id && x.AuthorId == request.AuthorId).SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(UserEntity), $"{request.Id} - {request.AuthorId}");

        if (entity.AuthorId != request.AuthorId)
            throw new ForbiddenAccessException();

        entity.Publish();

        await _context.SaveChangesAsync(cancellationToken);
    }
}
