using System.Data;
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

    public async Task<List<CardDal>> GetCardsAsync(string categoryName, bool isUserCategory, int count)
    {
        var cards = await _context.Cards
            .Include(c => c.Category)
            .Where(c => c.Category.Name == categoryName && c.Category.IsUserCategory == isUserCategory)
            .OrderBy(r => EF.Functions.Random())
            .ToListAsync();
        if (cards == null)
            throw new KeyNotFoundException("Card with this category not found.");

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
        if (card == null)
            throw new KeyNotFoundException("Card not found.");
        return card;
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

    public async Task InsertCardWithCategoryAsync(CardDal cardDal)
    {
        if (cardDal.Category.IsUserCategory == false)
            throw new ArgumentException("You can insert only user categories.");

        if (cardDal.Id != null || cardDal.Category.Id != null)
            throw new ArgumentException("Card must have no ids when inserting to db.");

        if (cardDal.Grades != null && cardDal.Grades.Count > 0)
            throw new ArgumentException("Card must have no grades when inserting to db.");

        var category = await _context.Categories.FirstOrDefaultAsync(cat => cat.Name == cardDal.Category.Name &&
                                                                            cat.IsUserCategory ==
                                                                            cardDal.Category.IsUserCategory);

        if (category != null)
            if (await _context.Cards.FirstOrDefaultAsync(card =>
                    card.CategoryId == category.Id && card.FirstSide == cardDal.FirstSide) != null)
                throw new DuplicateNameException("Card already exists.");
            else
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