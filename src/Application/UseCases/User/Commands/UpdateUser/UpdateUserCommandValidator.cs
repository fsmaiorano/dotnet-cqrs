using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.User.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    private readonly IBlogDataContext _context;

    public UpdateUserCommandValidator(IBlogDataContext context)
    {
        _context = context;

        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Email)
            .EmailAddress()
            .MustAsync(BeUniqueEmail);

        RuleFor(x => x.PasswordHash)
            .MinimumLength(8)
            .MaximumLength(50);

        RuleFor(x => x.Bio)
            .MaximumLength(500);

        RuleFor(x => x.Image)
            .MaximumLength(100);

        RuleFor(x => x.Slug)
            .MaximumLength(50);
    }

    public async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
    {
        return await _context.Users.AllAsync(l => l.Email != email, cancellationToken);
    }
}
