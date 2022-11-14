using ProCards.DAL.Models;

namespace ProCards.DAL.Interfaces;

public interface ICardRepository: IDisposable
{
    Task<List<CardDal>> GetCardsAsync(string categoryName, bool isUserCategory, int count);
    Task<CardDal> GetCardAsync(string categoryName, bool isUserCategory);
    Task InsertCardWithCategoryAsync(CardDal cardDal);
    Task SaveAsync();
}