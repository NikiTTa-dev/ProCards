using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ProCards.DAL;
using ProCards.Web.Attributes;

namespace ProCards.Web.Data.DTOs;

[CardGrade]
public class CardDto
{
    [Required]
    [JsonPropertyName("firstSide")]
    [MaxLength(ConfigurationConstants.MaxCardSideLength)]
    public string? FirstSide { get; init; }
    
    [Required]
    [JsonPropertyName("secondSide")]
    [MaxLength(ConfigurationConstants.MaxCardSideLength)]
    public string? SecondSide { get; init; }
    
    [Required]
    [JsonPropertyName("cardCategory")]
    public CategoryDto? CardCategory { get; init; }
    
    [JsonPropertyName("grade")]
    public GradeDto? Grade { get; init; }
}