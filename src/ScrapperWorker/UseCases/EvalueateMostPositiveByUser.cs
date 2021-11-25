using Microsoft.Extensions.Options;
using Scrapper.Application.ReviewsEvaluation.RankByUsers;
using Scrapper.Application.Scrappers.DealerRater;
using Spectre.Console;

namespace ScrapperWorker;
public class EvalueateMostPositiveByUser : BackgroundService
{
    private readonly ILogger<EvalueateMostPositiveByUser> _logger;
    private readonly DealerRaterScrapper _scrapper;
    private readonly DealerRaterOptions _options;


    public EvalueateMostPositiveByUser(ILogger<EvalueateMostPositiveByUser> logger, DealerRaterScrapper scrapper, IOptions<DealerRaterOptions> options)
    {
        _logger = logger;
        _scrapper = scrapper;
        _options = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogDebug("Initializing EvalueateMostPositiveByUser");

        var ranked = _scrapper.GetReviewsAsync().RankByUsers(take: _options.Rank);

        var table = new Table()
                        .Centered()
                        .Border(TableBorder.Rounded);

        table.AddColumn("Username");
        table.AddColumn("Total Reviews");
        table.AddColumn("Sum of Ratings");

        await AnsiConsole.Status()
            .StartAsync("[yellow]Evaluating reviews using RankByUsers...[/]", async ctx =>
            {
                await foreach (var item in ranked)
                {
                    table.AddRow(new Markup($"[blue]{item.User}[/]"),
                    new Markup($"[green]{item.TotalReviews}[/]"),
                    new Markup($"[red]{item.SumOfRatings}[/]")
                    );
                }

                ctx.Refresh();
            });

        AnsiConsole.Write(table);

        _logger.LogWarning("Press CTRL+C to finish");
        _logger.LogDebug("EvalueateMostPositiveByUser Executed");
    }
}
