using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ProCards.DAL.Models;

public class GradeDal: ModelBase
{
    public int GradeNumber { get; init; }
    
    [ForeignKey("Card")]
    public int? CardId { get; init; }
    
    [NotNull]
    public CardDal? Card { get; init; }
    
    public DateTime? PublishedAt { get; init; }
}