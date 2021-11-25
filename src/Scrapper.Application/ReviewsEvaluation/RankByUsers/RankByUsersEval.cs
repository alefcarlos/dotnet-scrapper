namespace Scrapper.Application.ReviewsEvaluation.RankByUsers;

public static class RankByUsersEval
{
    public static IAsyncEnumerable<RankedByUser> GroupByUsers(this IAsyncEnumerable<ReviewEntry> source)
    {
        return source.GroupBy(r => r.User)
            .SelectAwait(async g => new RankedByUser(g.Key, await g.SumAsync(x => x.Rating)))
            .OrderByDescending(x => x.TotalRating);
    }

    public static IAsyncEnumerable<RankedByUser> RankByUsers(this IAsyncEnumerable<ReviewEntry> source, int take = 3)
    {
        return source.GroupByUsers().Take(take);
    }
}
