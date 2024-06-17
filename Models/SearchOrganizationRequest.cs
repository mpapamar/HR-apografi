namespace HR.Apografi.Models
{
    public class SearchOrganizationRequest
    {
        public string? Code { get; set; }
        public string? PreferredLabel { get; set; }
        public string? SubOrganizationOf { get; set; }
        public int? PurposeId { get; set; }
        public int? OrganizationType { get; set; }
        public int? SpatialCountryId { get; set; }
        public int? SpatialMunicipalityId { get; set; }
    }
}
