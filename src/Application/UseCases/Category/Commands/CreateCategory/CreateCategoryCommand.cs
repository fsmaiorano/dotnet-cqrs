using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events.Category;
using MediatR;

namespace Application.UseCases.Category.Commands.CreateCategory;

public record CreateCategoryCommand : IRequest<int>
{
    public string? Name { get; set; }
    public string? Slug { get; set; }
}

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
{
    private readonly IBlogDataContext _context;

    public CreateCategoryCommandHandler(IBlogDataContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = new CategoryEntity(name: request.Name!)
            {
                Slug = request.Slug
            };

            entity.AddDomainEvent(new CategoryCreatedEvent(entity));

            _context.Categories.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
