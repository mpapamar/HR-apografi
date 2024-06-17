using System.Text.Json.Serialization;

namespace HR.Apografi.Models
{
    public class ExtraFieldValues
    {
        [JsonPropertyName("vacancyOpeningType")]
        public string VacancyOpeningType { get; set; }
    }
}
