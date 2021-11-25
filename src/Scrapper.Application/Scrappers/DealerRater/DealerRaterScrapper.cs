using AngleSharp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;

namespace Scrapper.Application.Scrappers.DealerRater;
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

    public IAsyncEnumerable<ReviewEntry> GetReviewsAsync()
    {
        _logger.LogDebug("Starting DealerRaterScrapper using configurations: BaseUrl: {Url} PageCount: {PageCount}", _options.DealerUrl, _options.PageCount);

        return Enumerable.Range(1, _options.PageCount).Select(GetReviewsAsync).Merge();
    }

    public async IAsyncEnumerable<ReviewEntry> GetReviewsAsync(int currentPage)
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

    private string BuildUrl(int page)
    {
        var baseUrlBuilder = new StringBuilder(_options.DealerUrl);

        if (!_options.DealerUrl.EndsWith("/"))
            baseUrlBuilder.Append('/');

        return $"{baseUrlBuilder}page{page}/?filter=ONLY_POSITIVE#link";
    }
}
