using AngleSharp.Dom;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("Scrapper.Application.Tests")]

namespace Scrapper.Application.DealerRater;

internal static class DealerRaterReviewParser
{
    static readonly Regex ratingClassRegex = new(@"rating-[0-9]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    static internal bool IsRatingClass(string input)
    {
        return ratingClassRegex.IsMatch(input);
    }

    internal static ReviewEntry ParseReview(this IElement review)
    {
        var reviewSummary = review.QuerySelector(".review-date")!;
        var reviewWrapper = review.QuerySelector(".review-wrapper")!;
        var reviewTittle = reviewWrapper.QuerySelector("div:nth-of-type(1)")!;
        var reviewBody = reviewWrapper.QuerySelector("div:nth-of-type(2)")!;
        var ratingElement = reviewSummary.QuerySelector(".dealership-rating div:nth-of-type(1)")!;
        var ratingDetail = review.QuerySelectorAll(".review-ratings-all .table .tr")!;

        var date = reviewSummary.QuerySelector("div:nth-of-type(1)")!.Text();
        var user = reviewTittle.QuerySelector("span")!.Text()[2..];
        var title = reviewTittle.QuerySelector("h3")!.Text();
        var content = reviewBody.QuerySelector(".tr .td p")!.Text();

        var entry = new ReviewEntry(date, user, title, content, ratingElement.ParseRating());

        foreach (var item in ratingDetail)
        {
            var rating = item.QuerySelector(".rating-static-indv");
            
            if (rating is null)
                continue;

            var name = item.QuerySelector(".lt-grey")!.Text();

            entry.AddDetail(name, rating.ParseRating());
        }

        return entry;
    }

    internal static decimal ParseRating(this IElement targetRatingElement)
    {
        var ratingClass = targetRatingElement.ClassList.Where(IsRatingClass).First();
        return decimal.Parse(ratingClass[7..]) / 10m;
    }
}

