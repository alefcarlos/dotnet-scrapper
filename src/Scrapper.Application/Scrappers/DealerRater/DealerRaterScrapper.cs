using AngleSharp;
using AngleSharp.Dom;
using Microsoft.Extensions.Options;
using Scrapper.Application.Dtos;
using System.Text;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("Scrapper.Application.Tests")]

namespace Scrapper.Application.Scrappers.DealerRater
{
    public class DealerRaterScrapper
    {
        private readonly IBrowsingContext _context;
        private readonly DealerRaterOptions _options;

        static readonly Regex ratingClassRegex = new(@"rating-[0-9]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public DealerRaterScrapper(IOptions<DealerRaterOptions> options)
        {
            var config = Configuration.Default.WithDefaultLoader();
            _context = BrowsingContext.New(config);

            _options = options.Value;
        }

        public async IAsyncEnumerable<ReviewEntry> GetReviewsAsync()
        {
            var enumerables = Enumerable.Range(1, _options.PageCount).Select(GetReviewsAsync);

            await foreach (var streams in Zip(enumerables))
            {
                foreach (var review in streams)
                {
                    yield return review;
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

        internal static ReviewEntry ParseReview(IElement review)
        {
            var reviewSummary = review.QuerySelector(".review-date")!;
            var reviewWrapper = review.QuerySelector(".review-wrapper")!;
            var reviewTittle = reviewWrapper.QuerySelector("div:nth-of-type(1)")!;
            var reviewBody = reviewWrapper.QuerySelector("div:nth-of-type(2)")!;

            var ratingElement = reviewSummary.QuerySelector(".dealership-rating div:nth-of-type(1)")!;
            var ratingClass = ratingElement.ClassList.Where(c => ratingClassRegex.IsMatch(c)).First();
            var rating = decimal.Parse(ratingClass[7..]) / 10m;

            var date = reviewSummary.QuerySelector("div:nth-of-type(1)")!.Text();
            var user = reviewTittle.QuerySelector("span")!.Text()[2..];
            var title = reviewTittle.QuerySelector("h3")!.Text();
            var content = reviewBody.QuerySelector(".tr .td p")!.Text();

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
