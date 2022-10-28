using ProCards.DAL.Models;

namespace ProCards.Core.Data.Interfaces;

public interface ICardRepository: IDisposable
{
    IEnumerable<Card> GetFiveCards(int firstCardId, string categoryName);
    Card GetCardById(int cardId);
    Category GetCategoryById(int categoryId);
    int InsertCardWithCategory(Card card, Category category);
    void Save();
}