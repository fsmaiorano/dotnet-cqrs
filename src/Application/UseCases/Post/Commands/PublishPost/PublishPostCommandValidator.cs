using Application.UseCases.Post.Commands.UpdatePost;
using FluentValidation;

namespace Application.UseCases.Post.Commands.PublishPost;
public class PublishPostCommandValidator : AbstractValidator<PublishPostCommand>
{
    public PublishPostCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.AuthorId)
        .NotEmpty();
    }
}
