using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ProCards.DAL;

namespace ProCards.Web.Data.DTOs;

public class CardDto : IValidatableObject
{
    [JsonPropertyName("id")]
    public int? Id { get; init; }

    [Required]
    [JsonPropertyName("firstSide")]
    [MaxLength(ConfigurationConstants.MaxCardSideLength)]
    public string FirstSide { get; init; }

    [Required]
    [JsonPropertyName("secondSide")]
    [MaxLength(ConfigurationConstants.MaxCardSideLength)]
    public string SecondSide { get; init; }

    [Required]
    [JsonPropertyName("cardCategory")]
    public CategoryDto Category { get; init; }

    [JsonPropertyName("grades")]
    public List<int> Grades { get; init; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Grades != null)
        {
            foreach (var grade in Grades)
            {
                if (grade < 1 || grade > 5)
                    yield return new ValidationResult(
                        $"{nameof(Grades)} must be 1 <= grade <= 5.",
                        new[] { nameof(Grades) });
            }

            if (Grades.Count > 20)
                yield return new ValidationResult(
                    $"{nameof(Grades)} count must be lower than 20.",
                    new[] { nameof(Grades) });
        }
    }
}