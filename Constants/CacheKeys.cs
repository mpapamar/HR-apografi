namespace HR.Apografi.Constants
{
    public static class CacheKeys
    {
        public const string Organizations = "orgs";
        public const string OrganizationsDiavgeia = "orgsd";
        public static string OrganizationsDiavgeiaPositions(string orgId) => $"orgs:{orgId}:Pos";
        public static string OrganizationsDiavgeiaDiorismoi(string orgId) => $"orgs:{orgId}:dior";
        public static string OrganizationsDiavgeiaProkiriksi(string orgId) => $"orgs:{orgId}:prok";
        public static string OrganizationsDiavgeiaApotelesmata(string orgId) => $"orgs:{orgId}:results";
        public static string DecisionById(string ada) => $"dec:{ada}";
        public static string OrganizationById(string code) => $"org:{code}";
        public const string OrganizationTypes = "orgTypes";
        public const string Functions = "functions";
        public const string Countries = "countries";
        public const string Cities = "cities";
    }
}
