using System.Text.Json.Serialization;

namespace HR.Apografi.Models
{
    public class Decision
    {
        [JsonPropertyName("protocolNumber")]
        public string ProtocolNumber { get; set; }

        [JsonPropertyName("subject")]
        public string Subject { get; set; }

        [JsonPropertyName("issueDate")]
        public long IssueDate { get; set; }

        [JsonPropertyName("organizationId")]
        public string OrganizationId { get; set; }

        [JsonPropertyName("decisionTypeId")]
        public string DecisionTypeId { get; set; }

        [JsonPropertyName("extraFieldValues")]
        public ExtraFieldValues ExtraFieldValues { get; set; }

        [JsonPropertyName("ada")]
        public string Ada { get; set; }

        [JsonPropertyName("publishTimestamp")]
        public long PublishTimestamp { get; set; }

        [JsonPropertyName("submissionTimestamp")]
        public long SubmissionTimestamp { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("documentUrl")]
        public string DocumentUrl { get; set; }
    }
}
