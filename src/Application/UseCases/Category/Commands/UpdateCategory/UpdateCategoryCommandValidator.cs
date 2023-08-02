using Application.Common.Interfaces;
using FluentValidation;

namespace Application.UseCases.Category.Commands.UpdateCategory;
public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator(IBlogDataContext context)
    {
        RuleFor(x => x.Id)
                .NotEmpty();

        RuleFor(x => x.Name)
            .MaximumLength(50);

        RuleFor(x => x.Slug)
            .MaximumLength(50);
    }
}
