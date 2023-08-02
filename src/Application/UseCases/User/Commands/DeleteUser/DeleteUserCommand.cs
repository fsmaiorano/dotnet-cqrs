using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events.User;
using MediatR;

namespace Application.UseCases.User.Commands.DeleteUser;

public record DeleteUserCommand(int Id) : IRequest;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IBlogDataContext _context;
    public DeleteUserCommandHandler(IBlogDataContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Users.FindAsync(request.Id) ??
                           throw new Exception("User not found");

        _context.Users.Remove(entity);

        entity.AddDomainEvent(new UserDeletedvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}
