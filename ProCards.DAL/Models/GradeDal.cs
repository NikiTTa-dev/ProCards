using System.ComponentModel.DataAnnotations.Schema;

namespace ProCards.DAL.Models;

public class GradeDal: ModelBase<int>
{
    public int GradeNumber { get; init; }
    
    [ForeignKey("Card")]
    public string? CardId { get; set; }
    
    public CardDal? Card { get; init; }
    
    public DateTime? PublishedAt { get; init; }
}