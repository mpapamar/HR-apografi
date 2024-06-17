
using HR.Apografi.Services;

namespace HR.Apografi.Workers
{
    public class DictionariesBackgroundWorker : BackgroundService
    {
        private readonly ILogger<DictionariesBackgroundWorker> _logger;
        private readonly ApografiService _apografiService;

        public DictionariesBackgroundWorker(ILogger<DictionariesBackgroundWorker> logger, ApografiService apografiService)
        {
            _logger = logger;
            _apografiService = apografiService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var tasks = new List<Task>
                {
                    _apografiService.GetCitiesAsync(),
                    _apografiService.GetFunctionsAsync(),
                    _apografiService.GetCountriesAsync(),
                    _apografiService.GetOrganizationTypesAsync()
                };

                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something bad happened. {ex.Message}");
                throw;
            }

            _logger.LogInformation("dictionaries fetch completed.");
        }
    }
}
