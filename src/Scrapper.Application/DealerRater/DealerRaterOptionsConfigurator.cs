using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;

namespace Scrapper.Application.DealerRater;

[ExcludeFromCodeCoverage]
public class DealerRaterOptionsConfigurator : IConfigureOptions<DealerRaterOptions>
{
    private readonly IConfiguration Configuration;

    public DealerRaterOptionsConfigurator(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void Configure(DealerRaterOptions options)
    {
        Configuration.GetSection("DealerRater").Bind(options);
    }
}

