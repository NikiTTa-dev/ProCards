using ProCards.DAL.Models;

namespace ProCards.Core.Data.Interfaces;

public interface ICategoryRepository
{
    IEnumerable<Category> GetFiveCategories(int firstCategoryId);
    Category GetCategoryById(int categoryId);
    void Save();
}