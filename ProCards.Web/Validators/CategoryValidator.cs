using FluentValidation;
using ProCards.DAL;
using ProCards.Web.Data.DTOs;

namespace ProCards.Web.Validators;

public class CategoryValidator: AbstractValidator<CategoryDto>
{
    public CategoryValidator()
    {
        RuleSet("PostCard", () =>
        {
            RuleFor(c => c.Id).Null();
            RuleFor(c => c.Name).NotNull().MaximumLength(ConfigurationConstants.MaxCategoryNameLength);
            RuleFor(c => c.IsUserCategory).Must(p=> p ?? true).NotNull();
        });
    }
}