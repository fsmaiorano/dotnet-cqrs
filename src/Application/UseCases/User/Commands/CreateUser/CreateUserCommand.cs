using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events.User;
using MediatR;

namespace Application.UseCases.User.Commands.CreateUser;

public record CreateUserCommand : IRequest<int>
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public string? Bio { get; set; }
    public string? Image { get; set; }
    public string? Slug { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly IBlogDataContext _context;

    public CreateUserCommandHandler(IBlogDataContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = new UserEntity
        {
            Bio = request.Bio,
            Email = request.Email,
            Image = request.Image,
            Name = request.Name,
            PasswordHash = request.PasswordHash,
            Slug = request.Slug
        };

        entity.AddDomainEvent(new UserCreatedEvent(entity));

        _context.Users.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}