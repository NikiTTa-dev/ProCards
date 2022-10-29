using ProCards.DAL.Models;

namespace ProCards.Core.Data.Interfaces;

public interface ICategoryRepository: IDisposable
{
    IEnumerable<Category> GetNineCategories(int firstCategoryId);
    Category GetCategoryById(int categoryId);
    int InsertCategory(Category name);
    void Save();
}