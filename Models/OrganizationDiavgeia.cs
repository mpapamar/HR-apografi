using System.Text.Json.Serialization;

namespace HR.Apografi.Models
{
    public class OrganizationDiavgeia
    {
        [JsonPropertyName("uid")]
        public string Id { get; set; }

        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("vatNumber")]
        public string VatNumber { get; set; }
    }
}
