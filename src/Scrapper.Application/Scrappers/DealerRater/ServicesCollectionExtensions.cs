using Scrapper.Application.Scrappers.DealerRater;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServicesCollectionExtensions
{
    public static IServiceCollection AddDealerRaterScrapper(this IServiceCollection services)
    {
        services.AddSingleton<DealerRaterScrapper>();
        services.ConfigureOptions<DealerRaterOptionsConfigurator>();

        return services;
    }
}

