using FluentValidation;

namespace Application.UseCases.Post.Commands.CreatePost;
public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(x => x.CategoryId)
                .NotEmpty();

        RuleFor(x => x.AuthorId)
        .NotEmpty();

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Summary)
            .MaximumLength(50);

        RuleFor(x => x.Body)
            .NotEmpty();

        RuleFor(x => x.Slug)
            .NotEmpty()
            .MaximumLength(50);
    }
}
