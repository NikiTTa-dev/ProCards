using ProCards.DAL.Models;

namespace ProCards.DAL.Interfaces;

public interface ICategoryRepository: IDisposable
{
    Task<List<CategoryDal>> GetNineUserCategories(int firstCategoryId);
}