namespace Scrapper.Application;

public record ReviewEntry(string Date, string User, string Title, string Content, decimal Rating)
{
    private readonly RatingDetails _ratingDetails = new();

    public void AddDetail(string name, decimal rating)
    {
        _ratingDetails.Add(name, rating);
    }

    public IReadOnlyDictionary<string, decimal> GetDetails() => _ratingDetails.AsReadOnly();

    public decimal DetailRatingsSum() => GetDetails().Sum(x => x.Value);
    public decimal DetailRatingsMean()
    {
        var details = GetDetails();

        return details.Sum(x => x.Value) / details.Count;
    }
}

public class RatingDetails : IEquatable<RatingDetails>
{
    private readonly Dictionary<string, decimal> _ratingDetails = new();

    public void Add(string name, decimal rating)
    {
        _ratingDetails.Add(name, rating);
    }

    public IReadOnlyDictionary<string, decimal> AsReadOnly() => _ratingDetails;

    public bool Equals(RatingDetails? other)
    {
        if (other is null)
            return false;

        return _ratingDetails.Keys.SequenceEqual(other!._ratingDetails.Keys);
    }
}
