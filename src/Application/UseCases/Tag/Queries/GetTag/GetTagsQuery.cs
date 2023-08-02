using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Tag.Queries.GetTag;

public record GetTagQuery : IRequest<List<TagEntity>>;

public class GetTagQueryHandler : IRequestHandler<GetTagQuery, List<TagEntity>>
{
    private readonly IBlogDataContext _context;

    public GetTagQueryHandler(IBlogDataContext context)
    {
        _context = context;
    }

    public async Task<List<TagEntity>> Handle(GetTagQuery request, CancellationToken cancellationToken)
    {
        return await _context.Tags.ToListAsync(cancellationToken);
    }
}
