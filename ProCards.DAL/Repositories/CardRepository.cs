using Microsoft.EntityFrameworkCore;
using ProCards.DAL.Context;
using ProCards.DAL.Exceptions;
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

    public async Task<List<CardDal>> GetCardsAsync(string categoryName, bool isUserCategory, int count)
    {
        var cards = await _context.Cards
            .Include(c => c.Category)
            .Where(c => c.Category.Name == categoryName && c.Category.IsUserCategory == isUserCategory)
            .OrderBy(r => EF.Functions.Random())
            .ToListAsync();

        return cards
            .Take(Math.Min(cards.Count, count))
            .ToList();
    }

    public async Task<CardDal> GetCardByCategoryAsync(string categoryName, bool isUserCategory)
    {
        var card = await _context.Cards
            .Include(c => c.Category)
            .Where(c => c.Category.Name == categoryName && c.Category.IsUserCategory == isUserCategory)
            .OrderBy(r => EF.Functions.Random())
            .FirstOrDefaultAsync();

        return card!;
    }

    public async Task<bool> IsCardExists(string categoryName, bool isUserCategory, string cardFirstSide)
    {
        if (await _context.Cards
                .Include(card => card.Category)
                .FirstOrDefaultAsync(card =>
                    card.Category.Name == categoryName &&
                    card.Category.IsUserCategory == isUserCategory &&
                    card.FirstSide == cardFirstSide)
            != null)
            return true;

        return false;
    }

    public async Task<bool> IsCardExists(int cardId, int categoryId)
    {
        return await _context.Categories.FindAsync(categoryId) != null &&
             await _context.Cards.FindAsync(cardId) != null;
    }

    public async Task InsertCardWithCategoryAsync(CardDal cardDal)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(cat => cat.Name == cardDal.Category.Name &&
                                        cat.IsUserCategory ==
                                        cardDal.Category.IsUserCategory);

        if (category != null)
            category.Cards = new List<CardDal> { cardDal };
        else
            _context.Cards.Add(cardDal);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
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