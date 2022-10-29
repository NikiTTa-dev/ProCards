using ProCards.Core.Data.Interfaces;
using ProCards.DAL.Context;
using ProCards.DAL.Models;

namespace ProCards.Core.Data.Repositories;

public class CategoryRepository: ICategoryRepository
{
    private AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public IEnumerable<Category> GetNineCategories(int firstCategoryId)
    {
        var categories = _context.Categories;
        var categoriesCount = categories.Count();
        
        if (categoriesCount < firstCategoryId)
            firstCategoryId = 1;
        
        if (categoriesCount - firstCategoryId + 1 < 9)
            firstCategoryId = categoriesCount - 8;
        
        return categories
            .Where(c => c.Id >= firstCategoryId)
            .Take(9)
            .ToList();
    }

    public Category GetCategoryById(int categoryId)
    {
        throw new NotImplementedException();
    }

    public int InsertCategory(Category name)
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