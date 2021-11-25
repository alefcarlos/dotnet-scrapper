namespace Scrapper.Application.Scrappers.DealerRater
{
    public class DealerRaterOptions
    {
        public string DealerUrl { get; set; } = default!;
        public int PageCount { get; set; } = 1;
        public int Rank { get; set; } = 1;
    }
}
