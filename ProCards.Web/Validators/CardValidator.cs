using System.Linq;
using FluentValidation;
using FluentValidation.Internal;
using ProCards.DAL;
using ProCards.DAL.Interfaces;
using ProCards.Web.Data.DTOs;

namespace ProCards.Web.Validators;

public class CardValidator : AbstractValidator<CardDto>
{
    public CardValidator(ICardRepository cardRepository)
    {
        RuleSet("PostCard", () =>
        {
            RuleFor(c => c.Grades).Null();
            RuleFor(c => c.Id).Null();
            RuleFor(c => c.FirstSide).NotNull().MaximumLength(ConfigurationConstants.MaxCardSideLength);
            RuleFor(c => c.SecondSide).NotNull().MaximumLength(ConfigurationConstants.MaxCardSideLength);
            RuleFor(c => c.Category).NotNull().SetValidator(new CategoryValidator(), "PostCard")
                .MustAsync(async (card, category, _) =>
                    !await cardRepository.IsCardExists(category.Name,
                        category.IsUserCategory ?? false,
                        card.FirstSide)).WithMessage(custom => "Card already exists")
                .When(c => c.Category != null &&
                           c.Category.IsUserCategory != null &&
                           c.Category.Name != null &&
                           c.FirstSide != null);
        });

        RuleSet("GetCard", () =>
        {
            RuleFor(c => c.Category).NotNull();
            RuleFor(c => c.Category.Id).NotNull().When(c => c.Category != null);
            RuleFor(c => c.Id)
                .NotNull()
                .MustAsync(async (c, id, _) => await cardRepository.IsCardExists(id ?? 1, c.Category.Id ?? 1))
                .When(c => c.Category != null && c.Category.Id != null);
            RuleFor(c => c.Grades)
                .NotNull()
                .Must(g => g.Count > 0 && g.Count <= 20)
                .Must(g =>
                {
                    var last = g.Last();
                    return last > 0 && last <= 5;
                });
        });
    }
}