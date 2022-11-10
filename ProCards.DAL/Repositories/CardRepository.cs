using Microsoft.EntityFrameworkCore;
using ProCards.DAL.Context;
using ProCards.DAL.Interfaces;
using ProCards.DAL.Models;

namespace ProCards.DAL.Repositories;

public class CardRepository : ICardRepository
{
    private readonly AppDbContext _context;

    public CardRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<CardDal> GetCards(string categoryName, bool isUserCategory, int count)
    {
        var cards = _context.Cards
            .Include(c => c.Category)
            .Where(c => c.Category.Name == categoryName && c.Category.IsUserCategory == isUserCategory)
            .OrderBy(r => EF.Functions.Random())
            .ToList();
        if (cards == null)
            throw new NullReferenceException("Card with this category not found.");

        return cards
            .Take(Math.Min(cards.Count(), count))
            .ToList();
    }

    public CardDal GetCard(string categoryName, bool isUserCategory)
    {
        var card = _context.Cards
            .Include(c => c.Category)
            .FirstOrDefault(c => c.Category.Name == categoryName && c.Category.IsUserCategory == isUserCategory);
        if (card == null)
            throw new NullReferenceException("Card not found.");
        return card;
    }

    public void InsertCardWithCategory(CardDal card)
    {
        _context.Cards.Add(card);
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    private bool _disposed;

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}