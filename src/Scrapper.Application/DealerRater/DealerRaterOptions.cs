namespace Scrapper.Application.DealerRater;

public class DealerRaterOptions
{
    public string BaseUrl { get; set; } = default!;
    public string Dealer { get; set; } = default!;
    public int PageCount { get; set; } = 1;
    public int Rank { get; set; } = 1;
    public decimal Threshold { get; set; } = 5;
}

