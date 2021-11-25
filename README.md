# dotnet-scrapper

It scrappes reviews from `www.dealerrater.com` and evaluates them ranked by Username and sum of POSITIVE ratings.
## How to run

### Configurations

It is possible to configure application using envs:

| Name                   | Description          | Default                                                                                                 |
| ---------------------- | -------------------- | ------------------------------------------------------------------------------------------------------- |
| DealerRater__PageCount | Total pages to fetch | 5                                                                                                       |
| DealerRater__DealerUrl | Dealer url           | https://www.dealerrater.com/dealer/McKaig-Chevrolet-Buick-A-Dealer-For-The-People-dealer-reviews-23685/ |
| DealerRater__Rank      | Rank Quantity        | 3                                                                                                       |

If you have `dotnet sdk`:

```
dotnet run --project .\src\ScrapperWorker\
```

or using docker

```
docker compose up
```

## How to run unit tests

If you have `dotnet sdk`:

```
dotnet test
```

or using docker

```
docker run --rm -v ${pwd}:/app -w /app/tests/Scrapper.Application.Tests mcr.microsoft.com/dotnet/sdk:6.0 dotnet test --logger:trx
```


