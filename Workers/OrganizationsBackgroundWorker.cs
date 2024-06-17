
using HR.Apografi.Services;

namespace HR.Apografi.Workers
{
    public class OrganizationsBackgroundWorker : BackgroundService
    {
        private readonly ILogger<OrganizationsBackgroundWorker> _logger;
        private readonly ApografiService _apografiService;
        private readonly DiavgeiaService _diavgeiaService;

        public OrganizationsBackgroundWorker(ILogger<OrganizationsBackgroundWorker> logger, ApografiService apografiService, DiavgeiaService diavgeiaService)
        {
            _logger = logger;
            _apografiService = apografiService;
            _diavgeiaService = diavgeiaService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var apografiOrgsTask = _apografiService.GetOrganizationsAsync();
                var diavgeiaOrgsTask = _diavgeiaService.GetOrganizationsAsync();
                await Task.WhenAll(apografiOrgsTask, diavgeiaOrgsTask);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something bad happened. {ex.Message}");
                throw;
            }

            _logger.LogInformation("Organizations fetch completed.");
        }
    }
}
