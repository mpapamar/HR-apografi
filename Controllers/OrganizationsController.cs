using HR.Apografi.Models;
using HR.Apografi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HR.Apografi.Controllers
{
    [Route("api/organizations")]
    public class OrganizationsController : Controller
    {
        private readonly ApografiService _apografiService;
        private readonly DiavgeiaService _diavgeiaService;

        public OrganizationsController(ApografiService apografiService, DiavgeiaService diavgeiaService)
        {
            _apografiService = apografiService;
            _diavgeiaService = diavgeiaService;
        }

        [HttpGet("organization-types")]
        public async Task<List<OrganizationType>> GetOrganizationTypes()
        {
            return await _apografiService.GetOrganizationTypesAsync();
        }

        [HttpGet("cities")]
        public async Task<List<City>> GetCities()
        {
            return await _apografiService.GetCitiesAsync();
        }

        [HttpGet("countries")]
        public async Task<List<Country>> GetCountries()
        {
            return await _apografiService.GetCountriesAsync();
        }

        [HttpGet("functions")]
        public async Task<List<Function>> GetFunctions()
        {
            return await _apografiService.GetFunctionsAsync();
        }

        [HttpGet("all")]
        public async Task<List<OrganizationLite>> GetOrganizations()
        {
            return await _apografiService.GetOrganizationsAsync();
        }

        [HttpGet("{code}")]
        public async Task<OrganizationDetailsViewModel> GetOrganization(string code)
        {
            var organizationTask = _apografiService.GetOrganizationAsync(code);
            var typesTask = _apografiService.GetOrganizationTypesAsync();
            var orgsTask = _apografiService.GetOrganizationsAsync();
            var citiesTask = _apografiService.GetCitiesAsync();

            await Task.WhenAll(organizationTask, typesTask, orgsTask, citiesTask);

            var organization = organizationTask.Result;
            var types = typesTask.Result;
            var cities = citiesTask.Result;
            var orgs = orgsTask.Result;
            var cityId = organization.Spatials?.FirstOrDefault()?.CityId;

            var url = string.IsNullOrEmpty(organization.Url) ? organization.Url : new UriBuilder(organization.Url).Uri.AbsoluteUri;

            return new OrganizationDetailsViewModel
            {
                Code = organization.Code,
                Label = organization.PreferredLabel,
                Vat = organization.VatId,
                OrganizationTypeLabel = types.FirstOrDefault(t => t.Id == organization.OrganizationType)?.Description!,
                SubOrganizationOfLabel = orgs.FirstOrDefault(o => o.Code == organization.SubOrganizationOf)?.PreferredLabel!,
                FoundationDate = organization.FoundationDate,
                Description = organization.Description,
                City = cityId.HasValue ? cities.FirstOrDefault(c => c.Id == cityId)?.Description : null!,
                PostCode = organization.MainAddress?.PostCode,
                FullAddress = organization.MainAddress?.FullAddress,
                Url = url,
                Telephone = organization.ContactPoint?.Telephone,
                Email = organization.ContactPoint?.Email
            };
        }

        [HttpPost("search")]
        public async Task<List<OrganizationLite>> GetOrganization([FromBody] SearchOrganizationRequest request)
        {
            var result = await _apografiService.SearchOrganizationsAsync(request);
            return result;
        }

        [HttpGet("prokiriksis/{orgId}")]
        public async Task<List<Decision>> GetProkiriksis(string orgId)
        {
            var result = await _diavgeiaService.GetOrganizationProkiriksi(orgId);
            return result;
        }

        [HttpGet("diorismoi/{orgId}")]
        public async Task<List<Decision>> GetDiorismoi(string orgId)
        {
            var result = await _diavgeiaService.GetOrganizationDiorismoi(orgId);
            return result;
        }

        [HttpGet("pinakes/{orgId}")]
        public async Task<List<Decision>> GetPinakes(string orgId)
        {
            var result = await _diavgeiaService.GetOrganizationPinakesEpitixonton(orgId);
            return result;
        }

        [HttpGet("decision-details/{ada}")]
        public async Task<Decision> GetDecision(string ada)
        {
            var result = await _diavgeiaService.GetDecision(ada);
            return result;
        }
    }
}
