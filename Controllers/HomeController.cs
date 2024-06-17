using HR.Apografi.Helpers;
using HR.Apografi.Models;
using HR.Apografi.Services;
using HR.Apografi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HR.Apografi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApografiService _apografiService;
        private readonly DiavgeiaService _diavgeiaService;
        private readonly OrganizationIdConverter _organizationIdConverter;

        public HomeController(ILogger<HomeController> logger, ApografiService apografiService, DiavgeiaService diavgeiaService, OrganizationIdConverter organizationIdConverter)
        {
            _logger = logger;
            _apografiService = apografiService;
            _diavgeiaService = diavgeiaService;
            _organizationIdConverter = organizationIdConverter;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Organizations()
        {
            return View();
        }

        public async Task<IActionResult> Positions([FromQuery(Name = "orgId")] string orgId)
        {
            var diavgeiaOrgId = await _organizationIdConverter.ConvertApografiToDiavgeiaAsync(orgId);
            var vm = new OrganizationPositionsViewModel
            {
                OrganizationId = orgId,
                OrganizationIdDiavgeia = diavgeiaOrgId,
                OrganizationName = (await _apografiService.GetOrganizationAsync(orgId)).PreferredLabel,
                Positions = []
            };

            if (!string.IsNullOrEmpty(diavgeiaOrgId))
            {
                vm.Positions = await _diavgeiaService.GetOrganizationPositions(diavgeiaOrgId);
            }

            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
