using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events.Tag;
using MediatR;

namespace Application.UseCases.Tag.Commands.CreateTag;

public record CreateTagCommand : IRequest<int>
{
    public string? Name { get; set; }
    public string? Slug { get; set; }
}

public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, int>
{
    private readonly IBlogDataContext _context;

    public CreateTagCommandHandler(IBlogDataContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = new TagEntity
            {
                Name = request.Name,
                Slug = request.Slug
            };

            entity.AddDomainEvent(new TagCreatedEvent(entity));

            _context.Tags.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
