using AngleSharp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;

namespace Scrapper.Application.DealerRater;
public class DealerRaterScrapper
{
    private readonly IBrowsingContext _context;
    private readonly DealerRaterOptions _options;
    private readonly ILogger _logger;

    public DealerRaterScrapper(IOptions<DealerRaterOptions> options, ILogger<DealerRaterScrapper> logger)
    {
        var config = Configuration.Default.WithDefaultLoader();
        _context = BrowsingContext.New(config);

        _options = options.Value;
        _logger = logger;
    }

    public IAsyncEnumerable<ReviewEntry> GetAsyncReviews()
    {
        _logger.LogDebug("Starting DealerRaterScrapper using configurations: BaseUrl: {Url} PageCount: {PageCount}", _options.BaseUrl, _options.PageCount);

        return Enumerable.Range(1, _options.PageCount).Select(GetAsyncReviews).Merge();
    }

    public async IAsyncEnumerable<ReviewEntry> GetAsyncReviews(int currentPage)
    {
        _logger.LogDebug("Fetching page {currentPage}", currentPage);

        var targetUrl = BuildUrl(currentPage);
        var document = await _context.OpenAsync(targetUrl);

        var reviews = document.QuerySelectorAll("#reviews .review-section .review-entry");

        foreach (var review in reviews)
        {
            yield return review.ParseReview();
        }
    }

    internal string BuildUrl(int page)
    {
        var urlBuilder = new StringBuilder(_options.BaseUrl);

        if (!_options.BaseUrl.EndsWith("/"))
            urlBuilder.Append('/');

        return $"{urlBuilder}{_options.Dealer}/page{page}/?filter=ONLY_POSITIVE#link";
    }
}
