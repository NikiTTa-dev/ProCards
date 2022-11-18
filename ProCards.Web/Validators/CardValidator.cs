using FluentValidation;
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
            RuleFor(c => c.FirstSide).NotNull();
            RuleFor(c => c.SecondSide).NotNull();
            RuleFor(c => c.Category).SetValidator(new CategoryValidator(), "PostCard");
            RuleFor(c => new { c.Category, c.FirstSide }).MustAsync(async (card, _) =>
                await cardRepository.IsCardExists(card.Category.Name,
                    card.Category.IsUserCategory ?? false,
                    card.FirstSide));
        });
        
        RuleSet("GetCard", () =>
        {
            
        });
    }
}