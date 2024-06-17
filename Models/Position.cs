using System.Text.Json.Serialization;

namespace HR.Apografi.Models
{
    public class Position
    {
        [JsonPropertyName("uid")]
        public string Id { get; set; }

        [JsonPropertyName("label")]
        public string Label { get; set; }
    }
}
