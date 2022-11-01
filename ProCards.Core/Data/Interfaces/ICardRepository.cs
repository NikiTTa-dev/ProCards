using ProCards.Core.Data.DTOs;
using ProCards.DAL.Models;

namespace ProCards.Core.Data.Interfaces;

public interface ICardRepository: IDisposable
{
    List<CardDto> GetFiveCards(string categoryName, bool isUserCategory);
    CardDto GetCard(string categoryName, bool isUserCategory);
    void InsertCardWithCategory(CardDto cardDto);
    void Save();
}