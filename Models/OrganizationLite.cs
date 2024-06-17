namespace HR.Apografi.Models
{
    public class OrganizationLite
    {
        public string Code { get; set; }

        public string PreferredLabel { get; set; }
        
        public string SubOrganizationOf { get; set; }
        
        public int OrganizationType { get; set; }
    }
}
