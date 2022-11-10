using ProCards.DAL.Models;

namespace ProCards.DAL.Interfaces;

public interface IGradeRepository: IDisposable
{
    GradeDal GetGradeById(int gradeId);
    void InsertGrade(GradeDal gradeDal);
    void Save();
}