using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.User.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    private readonly IBlogDataContext _context;

    public CreateUserCommandValidator(IBlogDataContext context)
    {
        _context = context;

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MustAsync(BeUniqueEmail);

        RuleFor(x => x.PasswordHash)
            .NotEmpty()
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
