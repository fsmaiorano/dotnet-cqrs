using Application.Common.Interfaces;
using Application.Common.Mapping;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Tag.Queries.GetTag;

public record GetTagWithPaginationQuery : IRequest<PaginatedList<TagEntity>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;

};

public class GetTagWithPaginationHandler : IRequestHandler<GetTagWithPaginationQuery, PaginatedList<TagEntity>>
{
    private readonly IMapper _mapper;
    private readonly IBlogDataContext _context;

    public GetTagWithPaginationHandler(IBlogDataContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<PaginatedList<TagEntity>> Handle(GetTagWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Tags
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
