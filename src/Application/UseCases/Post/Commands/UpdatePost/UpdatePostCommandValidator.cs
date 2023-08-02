using FluentValidation;

namespace Application.UseCases.Post.Commands.UpdatePost;
public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
{
    public UpdatePostCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Title)
            .MaximumLength(50);

        RuleFor(x => x.Summary)
            .MaximumLength(50);

        RuleFor(x => x.Slug)
            .MaximumLength(50);
    }
}
