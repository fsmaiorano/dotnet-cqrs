using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events.Tag;
using MediatR;

namespace Application.UseCases.Tag.Commands.DeleteTag;

public record DeleteTagCommand(int Id) : IRequest;

public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand>
{
    private readonly IBlogDataContext _context;
    public DeleteTagCommandHandler(IBlogDataContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Tags.FindAsync(request.Id) ??
                           throw new Exception("Tag not found");

        _context.Tags.Remove(entity);

        entity.AddDomainEvent(new TagDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}
