using ProCards.DAL.Models;

namespace ProCards.DAL.Interfaces;

public interface IGradeRepository: IDisposable
{
    Task InsertGradesAsync(List<GradeDal> gradeDals);
    Task SaveAsync();
}