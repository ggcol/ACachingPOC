using System.Text.Json.Serialization;

namespace Shared.Entities;

public class Programmer
{
    [JsonPropertyName(nameof(Id))] public int? Id { get; init; }
    [JsonPropertyName(nameof(Name))] public string? Name { get; set; }
    [JsonPropertyName(nameof(LinePerHour))] public int LinePerHour { get; set; }
    [JsonPropertyName(nameof(TimesInBurnout))] public int TimesInBurnout { get; set; }
}