using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using ProCards.DAL;

namespace ProCards.Web.Data.DTOs;

public class CardDto : IValidatableObject
{
    [JsonProperty(PropertyName ="id")]
    public int? Id { get; init; }

    [Required]
    [JsonProperty(PropertyName ="firstSide")]
    [MaxLength(ConfigurationConstants.MaxCardSideLength)]
    public string FirstSide { get; init; }

    [Required]
    [JsonProperty(PropertyName ="secondSide")]
    [MaxLength(ConfigurationConstants.MaxCardSideLength)]
    public string SecondSide { get; init; }

    [Required]
    [JsonProperty(PropertyName ="cardCategory")]
    public CategoryDto Category { get; init; }

    [JsonProperty(PropertyName ="grades")]
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