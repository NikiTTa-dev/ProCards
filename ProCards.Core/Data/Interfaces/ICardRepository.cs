using ProCards.DAL.Models;

namespace ProCards.Core.Data.Interfaces;

public interface ICardRepository: IDisposable
{
    IEnumerable<Card>? GetFiveCards(string categoryName, bool isUserCategory);
    Card GetCardById(int cardId);
    int InsertCardWithCategory(Card card, Category category);
    void Save();
}