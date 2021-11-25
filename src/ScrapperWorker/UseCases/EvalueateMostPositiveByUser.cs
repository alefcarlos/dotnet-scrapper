using Scrapper.Application.ReviewsEvaluation.RankByUsers;
using Scrapper.Application.Scrappers.DealerRater;
using Spectre.Console;

namespace ScrapperWorker
{
    public class EvalueateMostPositiveByUser : BackgroundService
    {
        private readonly ILogger<EvalueateMostPositiveByUser> _logger;
        private readonly DealerRaterScrapper _scrapper;

        public EvalueateMostPositiveByUser(ILogger<EvalueateMostPositiveByUser> logger, DealerRaterScrapper scrapper)
        {
            _logger = logger;
            _scrapper = scrapper;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("Initializing EvalueateMostPositiveByUser");

            var ranked = _scrapper.GetReviewsAsync().RankByUsers();

            // Create a table
            var table = new Table()
                            .Centered()
                            .Border(TableBorder.Rounded);

            // Add some columns
            table.AddColumn("Username");
            table.AddColumn("Sum of Rating");

            await AnsiConsole.Status()
                .StartAsync("[yellow]Evaluating RankByUsers...[/]", async ctx =>
                {
                    await foreach (var item in ranked)
                    {
                        table.AddRow(new Markup($"[blue]{item.User}[/]"), new Markup($"[green]{item.TotalRating}[/]"));
                    }

                    ctx.Refresh();
                });

            // Render the table to the console
            AnsiConsole.Write(table);

            _logger.LogInformation("Press CTRL+C to finish");
            _logger.LogDebug("EvalueateMostPositiveByUser Executed");
        }
    }
}