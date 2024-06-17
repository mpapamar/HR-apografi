using HR.Apografi.Services;

namespace HR.Apografi.Helpers
{
    public class OrganizationIdConverter
    {
        private readonly ApografiService _apografiService;
        private readonly DiavgeiaService _diavgeiaService;

        public OrganizationIdConverter(ApografiService apografiService, DiavgeiaService diavgeiaService)
        {
            _apografiService = apografiService;
            _diavgeiaService = diavgeiaService;
        }

        public async Task<string?> ConvertApografiToDiavgeiaAsync(string organizationId)
        {
            var apografiOrg = await _apografiService.GetOrganizationAsync(organizationId);
            var diavgeiaOrgs = await _diavgeiaService.GetOrganizationsAsync();
            var diavgeiaOrg = diavgeiaOrgs.FirstOrDefault(o => o.VatNumber == apografiOrg.VatId);

            if (diavgeiaOrg is null)
            {
                diavgeiaOrg = diavgeiaOrgs.FirstOrDefault(o => o.Label.Contains(apografiOrg.PreferredLabel, StringComparison.InvariantCultureIgnoreCase));
            }

            return diavgeiaOrg?.Id;
        }
    }
}
