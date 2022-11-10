using ProCards.DAL.Models;

namespace ProCards.DAL.Interfaces;

public interface ICategoryRepository: IDisposable
{
    IEnumerable<CategoryDal> GetNineUserCategories(int firstCategoryId);
    IEnumerable<CategoryDal> GetTenDefaultCategories();
    CategoryDal? GetCategoryById(int categoryId);
}