using Microsoft.EntityFrameworkCore;
using ProCards.DAL.Context;
using ProCards.DAL.Interfaces;
using ProCards.DAL.Models;

namespace ProCards.DAL.Repositories;

public class GradeRepository: IGradeRepository
{
    private AppDbContext _context;
    
    public GradeRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public GradeDal GetGradeById(int gradeId)
    {
        throw new NotImplementedException();
    }

    public void InsertGrade(GradeDal gradeDal)
    {
        _context.Grades.Add(gradeDal);
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