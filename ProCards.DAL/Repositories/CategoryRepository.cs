using Microsoft.EntityFrameworkCore;
using ProCards.DAL.Context;
using ProCards.DAL.Interfaces;
using ProCards.DAL.Models;

namespace ProCards.DAL.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<CategoryDal>> GetNineUserCategories(int firstCategoryId)
    {
        var categories = _context.Categories;
        var categoriesCount = await categories.CountAsync();

        if (categoriesCount < firstCategoryId)
            firstCategoryId = 1;

        if (categoriesCount - firstCategoryId + 1 < 9)
            firstCategoryId = categoriesCount - 8;

        return await categories
            .Where(c => c.Id >= firstCategoryId && c.IsUserCategory == true)
            .Take(9)
            .ToListAsync();
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