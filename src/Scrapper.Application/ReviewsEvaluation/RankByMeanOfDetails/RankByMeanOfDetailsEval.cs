namespace Scrapper.Application.ReviewsEvaluation.RankByUsers;

public static class RankByMeanOfDetailsEval
{
    /// <summary>
    /// 1. Select all ratings with 5
    /// 2. Sum all details ratings
    /// 3. Filter details ratings mean value
    /// </summary>
    /// <param name="source"></param>
    /// <param name="take"></param>
    public static IAsyncEnumerable<RankedByMeanOfDetails> RankByMeanOfDetails(this IAsyncEnumerable<ReviewEntry> source, int take = 3)
    {
        const int targetRating = 5;

        return source
                .Where(review => review.Rating == targetRating)
                .Where(review => review.DetailRatingsMean() < targetRating)
                .Select(review => new RankedByMeanOfDetails(review, targetRating - review.DetailRatingsMean()))
                .OrderByDescending(rank => rank.Difference)
                .Take(take);
    }

    public record RankedByMeanOfDetails(ReviewEntry Review, decimal Difference);
}
