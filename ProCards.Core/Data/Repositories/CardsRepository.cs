using Microsoft.EntityFrameworkCore;
using ProCards.Core.Data.Interfaces;
using ProCards.DAL.Context;
using ProCards.DAL.Models;

namespace ProCards.Core.Data.Repositories;

public class CardsRepository: ICardRepository
{
    private AppDbContext _context;
    
    public CardsRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Card> GetFiveCards(int firstCardId, string categoryName)
    {
        throw new NotImplementedException();
    }

    public Card GetCardById(int cardId)
    {
        throw new NotImplementedException();
    }

    public Category GetCategoryById(int categoryId)
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