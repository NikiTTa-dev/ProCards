using ProCards.DAL.Models;

namespace ProCards.DAL.Interfaces;

public interface ICardRepository: IDisposable
{
    List<CardDal> GetCards(string categoryName, bool isUserCategory, int count);
    CardDal GetCard(string categoryName, bool isUserCategory);
    void InsertCardWithCategory(CardDal card);
    void Save();
}