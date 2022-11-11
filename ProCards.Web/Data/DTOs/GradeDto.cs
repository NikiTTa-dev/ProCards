using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProCards.Web.Data.DTOs;

public class GradeDto
{
    [Required]
    [JsonPropertyName("gradeNumber")]
    [Range(1, 5)]
    public int? GradeNumber { get; init; }
    
    [Required]
    [JsonPropertyName("cardFirstSide")]
    public string CardFirstSide { get; init; }
}