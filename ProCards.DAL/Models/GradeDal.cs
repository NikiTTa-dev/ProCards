namespace ProCards.DAL.Models;

public class GradeDal: ModelBase
{
    public int GradeNumber { get; init; }
    
    public CardDal? Card { get; init; }
    
    public DateTime? PublishedAt { get; init; }
}