using Microsoft.EntityFrameworkCore;
using ProCards.Core.Data.DTOs;
using ProCards.Core.Data.Interfaces;
using ProCards.DAL.Context;
using ProCards.DAL.Models;

namespace ProCards.Core.Data.Repositories;

public class CardRepository : ICardRepository
{
    private AppDbContext _context;

    public CardRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<CardDto> GetFiveCards(string categoryName, bool isUserCategory)
    {
        var cards = _context.Cards
            .Include(c => c.Category);
        
        var randomFiveCards = cards
            .Where(c => c.Category.Name == categoryName && c.Category.IsUserCategory == isUserCategory)
            .OrderBy(r => EF.Functions.Random())
            .Take(Math.Min(cards.Count(), 5))
            .ToList();

        List<CardDto> cardDtos = new();
        foreach (var card in randomFiveCards)
        {
            cardDtos.Add(new CardDto
                {
                    FirstSide = card.FirstSide,
                    SecondSide = card.SecondSide,
                    CardCategory = new CategoryDto
                    {
                        Name = card.Category.Name
                    }
                }
            );
        }

        return cardDtos;
    }

    public CardDto GetCard(string categoryName, bool isUserCategory)
    {
        var card = _context.Cards
            .Include(c => c.Category)
            .FirstOrDefault(c => c.Category.Name == categoryName && c.Category.IsUserCategory == isUserCategory);
        if (card == null)
            throw new NullReferenceException("Card not found.");
        return new CardDto
        {
            FirstSide = card.FirstSide,
            SecondSide = card.SecondSide,
            CardCategory = new CategoryDto
            {
                Name = card.Category.Name
            }
        };
    }

    public void InsertCardWithCategory(CardDto cardDto)
    {
        Category category = _context.Categories
            .FirstOrDefault(cat => cat.Name == cardDto.CardCategory.Name);

        if (category == null)
            category = new Category
            {
                Name = cardDto.CardCategory.Name,
                IsUserCategory = true,
                PublishedAt = DateTime.Now
            };

        Card card = new Card
        {
            FirstSide = cardDto.FirstSide,
            SecondSide = cardDto.SecondSide,
            Category = category,
            PublishedAt = DateTime.Now
        };

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