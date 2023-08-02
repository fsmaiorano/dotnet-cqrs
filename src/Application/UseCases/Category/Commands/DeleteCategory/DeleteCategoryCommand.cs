using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events.Category;
using MediatR;

namespace Application.UseCases.Category.Commands.DeleteCategory;

public record DeleteCategoryCommand(int Id) : IRequest;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly IBlogDataContext _context;
    public DeleteCategoryCommandHandler(IBlogDataContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Categories.FindAsync(request.Id) ??
                           throw new Exception("Category not found");

        _context.Categories.Remove(entity);

        entity.AddDomainEvent(new CategoryDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}
