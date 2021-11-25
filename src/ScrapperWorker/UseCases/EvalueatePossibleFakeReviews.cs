using Microsoft.Extensions.Options;
using Scrapper.Application.DealerRater;
using Scrapper.Application.ReviewsEvaluation.RankByUsers;
using Spectre.Console;

namespace ScrapperWorker;
public class EvalueatePossibleFakeReviews : BackgroundService
{
    private readonly ILogger<EvalueatePossibleFakeReviews> _logger;
    private readonly DealerRaterScrapper _scrapper;
    private readonly DealerRaterOptions _options;


    public EvalueatePossibleFakeReviews(ILogger<EvalueatePossibleFakeReviews> logger, DealerRaterScrapper scrapper, IOptions<DealerRaterOptions> options)
    {
        _logger = logger;
        _scrapper = scrapper;
        _options = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogDebug("Initializing EvalueateMostPositiveByUser");

        var ranked = _scrapper.GetReviewsAsync().RankByMeanOfDetails(take: _options.Rank, threshold: _options.Threshold);

        var table = new Table()
                        .Centered()
                        .Border(TableBorder.Rounded);
        
        table.AddColumn("Username");
        table.AddColumn("Tittle");
        table.AddColumn("Rating");
        table.AddColumn("Detailed Rating Mean");
        table.AddColumn("Threshold");
        table.AddColumn("Difference");

        await AnsiConsole.Status()
            .StartAsync("[yellow]Evaluating reviews using RankByMeanOfDetails...[/]", async ctx =>
            {
                await foreach (var item in ranked)
                {
                    table.AddRow(new Markup($"[blue]{item.Review.User}[/]"),
                    new Markup($"[green]{item.Review.Title}[/]"),
                    new Markup($"[green]{item.Review.Rating}[/]"),
                    new Markup($"[green]{item.Review.DetailRatingsMean()}[/]"),
                    new Markup($"[red]{_options.Threshold}[/]"),
                    new Markup($"[red]{item.Difference}[/]")
                    );
                }

                ctx.Refresh();
            });

        AnsiConsole.Write(table);

        _logger.LogWarning("Press CTRL+C to finish");
        _logger.LogDebug("EvalueateMostPositiveByUser Executed");
    }
}
