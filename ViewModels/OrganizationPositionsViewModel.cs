using HR.Apografi.Models;

namespace HR.Apografi.ViewModels
{
    public class OrganizationPositionsViewModel
    {
        public string OrganizationId { get; set; }

        public string OrganizationIdDiavgeia { get; set; }

        public string OrganizationName { get; set; }

        public List<Position> Positions { get; set; }
    }
}
