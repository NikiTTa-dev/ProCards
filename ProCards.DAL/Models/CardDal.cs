using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ProCards.DAL.Models;

public class CardDal: ModelBase
{
    public string? FirstSide { get; init; }
    
    public string? SecondSide { get; init; }
    
    [ForeignKey("Category")]
    public int? CategoryId { get; init; }
    
    [NotNull]
    public CategoryDal? Category { get; init; }
    
    public DateTime? PublishedAt { get; init; }
    
    public ICollection<GradeDal>? Grades { get; set; }
}