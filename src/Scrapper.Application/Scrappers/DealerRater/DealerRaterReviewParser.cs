using AngleSharp.Dom;
using Scrapper.Application.Dtos;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("Scrapper.Application.Tests")]

namespace Scrapper.Application.Scrappers.DealerRater
{
    internal static class DealerRaterReviewParser
    {
        static readonly Regex ratingClassRegex = new(@"rating-[0-9]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        static internal bool IsRatingClass(string input) => ratingClassRegex.IsMatch(input);

        internal static ReviewEntry ParseReview(this IElement review)
        {
            var reviewSummary = review.QuerySelector(".review-date")!;
            var reviewWrapper = review.QuerySelector(".review-wrapper")!;
            var reviewTittle = reviewWrapper.QuerySelector("div:nth-of-type(1)")!;
            var reviewBody = reviewWrapper.QuerySelector("div:nth-of-type(2)")!;

            var ratingElement = reviewSummary.QuerySelector(".dealership-rating div:nth-of-type(1)")!;
            var ratingClass = ratingElement.ClassList.Where(IsRatingClass).First();
            var rating = decimal.Parse(ratingClass[7..]) / 10m;

            var date = reviewSummary.QuerySelector("div:nth-of-type(1)")!.Text();
            var user = reviewTittle.QuerySelector("span")!.Text()[2..];
            var title = reviewTittle.QuerySelector("h3")!.Text();
            var content = reviewBody.QuerySelector(".tr .td p")!.Text();

            return new ReviewEntry(date, user, title, content, rating);
        }
    }
}
