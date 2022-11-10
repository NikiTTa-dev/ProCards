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

    public IEnumerable<CategoryDal> GetNineUserCategories(int firstCategoryId)
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

    public IEnumerable<CategoryDal> GetTenDefaultCategories()
    {
        return _context.Categories;
    }

    public CategoryDal GetCategoryById(int categoryId)
    {
        var category = _context.Categories
            .FirstOrDefault(cat => cat.Id == categoryId);
        if (category == null)
            throw new NullReferenceException("Category not found.");
        return category;
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