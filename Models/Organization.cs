using System.Text.Json.Serialization;

namespace HR.Apografi.Models
{
    public class Organization
    {
        public string Code { get; set; }

        public string PreferredLabel { get; set; }

        public List<string> AlternativeLabels { get; set; }

        [JsonPropertyName("purpose")]
        public List<int> Purposes { get; set; }

        [JsonPropertyName("spatial")]
        public List<Spatial> Spatials { get; set; }

        public string SubOrganizationOf { get; set; }

        public int OrganizationType { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public ContactPoint ContactPoint { get; set; }

        public string VatId { get; set; }

        public string Status { get; set; }

        public string FoundationDate { get; set; }

        public MainAddress MainAddress { get; set; }
    }
}
