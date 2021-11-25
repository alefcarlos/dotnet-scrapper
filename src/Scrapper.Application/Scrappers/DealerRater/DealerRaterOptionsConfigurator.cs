using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Scrapper.Application.Scrappers.DealerRater;

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

