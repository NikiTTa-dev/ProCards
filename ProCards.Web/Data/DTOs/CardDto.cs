using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using ProCards.DAL;

namespace ProCards.Web.Data.DTOs;

public class CardDto
{
    [JsonProperty(PropertyName ="id")]
    public int? Id { get; init; }

    [JsonProperty(PropertyName ="firstSide")]
    public string FirstSide { get; init; }

    [JsonProperty(PropertyName ="secondSide")]
    public string SecondSide { get; init; }

    [JsonProperty(PropertyName ="category")]
    public CategoryDto Category { get; init; }

    [JsonProperty(PropertyName ="grades")]
    public List<int> Grades { get; init; }
}