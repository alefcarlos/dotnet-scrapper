using ScrapperWorker;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddDealerRaterScrapper();
        services.AddUseCases();
    })
    .Build();

await host.RunAsync();
