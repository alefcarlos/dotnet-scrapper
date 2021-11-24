using Scrapper.Application.Scrappers.DealerRater;

namespace ScrapperWorker
{
    public class ScrapperJob : BackgroundService
    {
        private readonly ILogger<ScrapperJob> _logger;
        private readonly DealerRaterScrapper _scrapper;

        public ScrapperJob(ILogger<ScrapperJob> logger, DealerRaterScrapper scrapper)
        {
            _logger = logger;
            _scrapper = scrapper;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Initializing DealerRaterScrapperJob");

            await foreach (var item in _scrapper.GetReviewsAsync())
            {
                _logger.LogInformation(item.Date);
            }

            _logger.LogInformation("DealerRaterScrapperJob Executed");
        }
    }
}