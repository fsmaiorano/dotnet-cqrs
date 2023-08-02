using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Category.Queries.GetCategory;

public record GetCategoryQuery : IRequest<List<CategoryEntity>>;

public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, List<CategoryEntity>>
{
    private readonly IBlogDataContext _context;

    public GetCategoryQueryHandler(IBlogDataContext context)
    {
        _context = context;
    }

    public async Task<List<CategoryEntity>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        return await _context.Categories.ToListAsync(cancellationToken);
    }
}
