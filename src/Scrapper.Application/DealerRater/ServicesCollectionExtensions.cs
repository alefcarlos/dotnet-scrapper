using Scrapper.Application.DealerRater;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Extensions.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class ServicesCollectionExtensions
{
    public static IServiceCollection AddDealerRaterScrapper(this IServiceCollection services)
    {
        services.AddSingleton<DealerRaterScrapper>();
        services.ConfigureOptions<DealerRaterOptionsConfigurator>();

        return services;
    }
}

