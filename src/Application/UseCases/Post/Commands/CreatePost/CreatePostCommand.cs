using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events.Post;
using MediatR;

namespace Application.UseCases.Post.Commands.CreatePost;

public record CreatePostCommand : IRequest<int>
{
    public int CategoryId { get; set; }
    public int AuthorId { get; set; }
    public string? Title { get; set; }
    public string? Summary { get; set; }
    public string? Body { get; set; }
    public string? Slug { get; set; }
    public List<int>? Tags { get; set; }
}

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, int>
{
    private readonly IBlogDataContext _context;

    public CreatePostCommandHandler(IBlogDataContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var entity = new PostEntity(
                        title: request.Title!,
                        body: request.Body!,
                        categoryId: request.CategoryId,
                        authorId: request.AuthorId)
        {
            Summary = request.Summary,
            Slug = request.Slug
        };

        if (request.Tags is not null && request.Tags.Any())
        {
            entity.Tags = new List<TagEntity>();

            foreach (var tagId in request.Tags)
            {
                var tag = await _context.Tags.FindAsync(new object?[] { tagId }, cancellationToken: cancellationToken);
                if (tag is not null)
                    entity.Tags.Add(tag);
            }
        }

        entity.AddDomainEvent(new PostCreatedEvent(entity));

        _context.Posts.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
