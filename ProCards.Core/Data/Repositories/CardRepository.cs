using Microsoft.EntityFrameworkCore;
using ProCards.Core.Data.Interfaces;
using ProCards.DAL.Context;
using ProCards.DAL.Models;

namespace ProCards.Core.Data.Repositories;

public class CardRepository: ICardRepository
{
    private AppDbContext _context;
    
    public CardRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Card> GetFiveCards(string categoryName, bool isUserCategory)
    {
        int rowsCount = _context.Cards.Count();
        return _context.Cards
            .Include(card => card.Category)
            .Where(card => card.Category.Name == categoryName && card.Category.IsUserCategory == isUserCategory)
            .OrderBy(r => EF.Functions.Random())
            .Take(Math.Min(rowsCount, 5))
            .ToList();
    }

    public Card GetCardById(int cardId)
    {
        throw new NotImplementedException();
    }

    public int InsertCardWithCategory(Card card, Category category)
    {
        throw new NotImplementedException();
    }

    public void Save()
    {
        throw new NotImplementedException();
    }
    
    private bool _disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this._disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        this._disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}