using FluentValidation;

namespace Application.UseCases.Tag.Commands.CreateTag;
public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
{
    public CreateTagCommandValidator()
    {

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Slug)
            .MaximumLength(50);
    }
}
