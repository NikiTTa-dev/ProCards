using ProCards.DAL.Models;

namespace ProCards.DAL.Interfaces;

public interface IGradeRepository: IDisposable
{
    Task InsertGradeAsync(GradeDal gradeDal);
    Task SaveAsync();
}