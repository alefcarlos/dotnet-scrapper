using ScrapperWorker;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServicesCollectionExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddHostedService<ScrapperJob>();

            return services;
        }
    }
}
