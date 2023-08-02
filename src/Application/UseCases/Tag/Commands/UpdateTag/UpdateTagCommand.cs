using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Tag.Commands.UpdateTag;

public record UpdateTagCommand : IRequest
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Slug { get; set; }
}

public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand>
{
    private readonly IBlogDataContext _context;

    public UpdateTagCommandHandler(IBlogDataContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Tags.FindAsync(new object[] { request.Id, }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(UserEntity), request.Id);
        }

        entity.Name = request.Name ?? entity.Name;
        entity.Slug = request.Slug ?? entity.Slug;

        await _context.SaveChangesAsync(cancellationToken);
    }
}