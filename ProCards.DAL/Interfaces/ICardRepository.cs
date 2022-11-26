using ProCards.DAL.Models;

namespace ProCards.DAL.Interfaces;

public interface ICardRepository: IDisposable
{
    Task<List<CardDal>> GetCardsAsync(string categoryName, bool isUserCategory, int count);
    Task<CardDal> GetCardByCategoryAsync(string categoryName, bool isUserCategory);
    Task<bool> IsCardExists(string categoryName, bool isUserCategory, string cardFirstSide);
    Task<bool> IsCardExists(int cardId, int categoryId);
    Task InsertCardWithCategoryAsync(CardDal cardDal);
    Task SaveAsync();
}