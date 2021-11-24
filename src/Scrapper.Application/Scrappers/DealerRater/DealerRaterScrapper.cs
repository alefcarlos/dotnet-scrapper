using AngleSharp;
using AngleSharp.Dom;
using Microsoft.Extensions.Options;
using Scrapper.Application.Dtos;
using System.Text;
using System.Runtime.CompilerServices;

namespace Scrapper.Application.Scrappers.DealerRater
{
    public class DealerRaterScrapper
    {
        private readonly IBrowsingContext _context;
        private readonly DealerRaterOptions _options;

        public DealerRaterScrapper(IOptions<DealerRaterOptions> options)
        {
            var config = Configuration.Default.WithDefaultLoader();
            _context = BrowsingContext.New(config);

            _options = options.Value;
        }

        public async IAsyncEnumerable<ReviewEntry> GetReviewsAsync()
        {
            var enumerables = Enumerable.Range(1, _options.PageCount).Select(GetReviewsAsync);

            await foreach (var item in Zip(enumerables))
            {
                foreach (var item2 in item)
                {
                   yield return item2;
                }
            }
        }

        public async IAsyncEnumerable<ReviewEntry> GetReviewsAsync(int currentPage)
        {
            var targetUrl = BuildUrl(currentPage);
            var document = await _context.OpenAsync(targetUrl);

            var reviews = document.QuerySelectorAll("#reviews .review-section .review-entry");

            foreach (var review in reviews)
            {
                yield return ParseReview(review);
            }
        }

        public static ReviewEntry ParseReview(IElement review)
        {
            var reviewSummary = review.QuerySelector(".review-date")!;
            var reviewWrapper = review.QuerySelector(".review-wrapper")!;
            var reviewTittle = reviewWrapper.QuerySelector("div:nth-of-type(1)")!;
            var reviewBody = reviewWrapper.QuerySelector("div:nth-of-type(2)")!;

            var date = reviewSummary.QuerySelector("div:nth-of-type(1)")!.Text();
            var user = reviewTittle.QuerySelector("span")!.Text()[2..];
            var title = reviewTittle.QuerySelector("h3")!.Text();
            var content = reviewBody.QuerySelector(".tr .td p")!.Text();
            var rating = 0;

            return new ReviewEntry(date, user, title, content, rating);
        }

        private string BuildUrl(int page)
        {
            var baseUrlBuilder = new StringBuilder(_options.BaseUrl);

            if (!_options.BaseUrl.EndsWith("/"))
                baseUrlBuilder.Append('/');

            return $"{baseUrlBuilder}page{page}/?filter=ONLY_POSITIVE#link";
        }

        public static async IAsyncEnumerable<TSource[]> Zip<TSource>(
            IEnumerable<IAsyncEnumerable<TSource>> sources,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var enumerators = sources
                .Select(x => x.GetAsyncEnumerator(cancellationToken))
                .ToArray();
            try
            {
                while (true)
                {
                    var array = new TSource[enumerators.Length];
                    for (int i = 0; i < enumerators.Length; i++)
                    {
                        if (!await enumerators[i].MoveNextAsync()) yield break;
                        array[i] = enumerators[i].Current;
                    }
                    yield return array;
                }
            }
            finally
            {
                foreach (var enumerator in enumerators)
                {
                    await enumerator.DisposeAsync();
                }
            }
        }
    }
}
