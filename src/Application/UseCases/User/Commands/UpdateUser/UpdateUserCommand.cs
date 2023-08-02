using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.User.Commands.UpdateUser;

public record UpdateUserCommand : IRequest
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public string? Bio { get; set; }
    public string? Image { get; set; }
    public string? Slug { get; set; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IBlogDataContext _context;

    public UpdateUserCommandHandler(IBlogDataContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Users.FindAsync(new object[] { request.Id, }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(UserEntity), request.Id);
        }

        entity.Bio = request.Bio ?? entity.Bio;
        entity.Email = request.Email ?? entity.Email;
        entity.Image = request.Image ?? entity.Image;
        entity.Name = request.Name ?? entity.Name;
        entity.PasswordHash = request.PasswordHash ?? entity.PasswordHash;
        entity.Slug = request.Slug ?? entity.Slug;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
