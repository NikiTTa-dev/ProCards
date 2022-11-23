using ProCards.DAL.Context;
using ProCards.DAL.Exceptions;
using ProCards.DAL.Interfaces;
using ProCards.DAL.Models;

namespace ProCards.DAL.Repositories;

public class GradeRepository: IGradeRepository
{
    private readonly AppDbContext _context;
    
    public GradeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task InsertGradesAsync(List<GradeDal> gradeDals)
    {
        var grade = gradeDals.FirstOrDefault();
        if (grade != null)
        {
            var card = await _context.Cards.FindAsync(grade.CardId);
            if (card == null)
                throw new CardNotFoundException("Grades can not be added to database because of card doesn't exist.");
            card.Grades = gradeDals;
        }
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