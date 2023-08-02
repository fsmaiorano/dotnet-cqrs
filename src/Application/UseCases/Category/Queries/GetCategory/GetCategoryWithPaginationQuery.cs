using Application.Common.Interfaces;
using Application.Common.Mapping;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Category.Queries.GetCategory;

public record GetCategoryWithPaginationQuery : IRequest<PaginatedList<CategoryEntity>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;

};

public class GetCategoryWithPaginationHandler : IRequestHandler<GetCategoryWithPaginationQuery, PaginatedList<CategoryEntity>>
{
    private readonly IMapper _mapper;
    private readonly IBlogDataContext _context;

    public GetCategoryWithPaginationHandler(IBlogDataContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<PaginatedList<CategoryEntity>> Handle(GetCategoryWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Categories
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
