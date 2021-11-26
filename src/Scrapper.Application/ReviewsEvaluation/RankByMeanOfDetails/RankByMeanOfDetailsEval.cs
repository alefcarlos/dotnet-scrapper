namespace Scrapper.Application.ReviewsEvaluation.RankByMeanOfDetails;

public static class RankByMeanOfDetailsEval
{
    /// <summary>
    /// 1. Select all ratings with 5
    /// 2. Sum all details ratings
    /// 3. Filter details ratings mean value by threshold
    /// </summary>
    /// <param name="source"></param>
    /// <param name="take"></param>
    public static IAsyncEnumerable<RankedByMeanOfDetails> RankByMeanOfDetails(this IAsyncEnumerable<ReviewEntry> source, int take = 3, decimal threshold = 5)
    {
        return source
                .Where(review => review.Rating == 5)
                .Where(review => review.DetailRatingsMean() < threshold)
                .Select(review => new RankedByMeanOfDetails(review, threshold - review.DetailRatingsMean()))
                .OrderByDescending(rank => rank.Difference)
                .Take(take);
    }

    public record RankedByMeanOfDetails(ReviewEntry Review, decimal Difference);
}
