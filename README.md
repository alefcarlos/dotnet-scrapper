# dotnet-scrapper

## Get started

It scrappes `N` review pages for an specific dealer and evaluates them to select the top `K` suspicious users with positve ratings.

```
docker compose up
```
### Configurations

It is possible to configure Scrapper using these envs:

| Name                   | Description          | Default                                                             |
| ---------------------- | -------------------- | ------------------------------------------------------------------- |
| DealerRater__PageCount | Total pages to fetch | 5                                                                   |
| DealerRater__BaseUrl   | Base url             | https://www.dealerrater.com/dealer/                                 |
| DealerRater__Dealer    | Dealer url           | McKaig-Chevrolet-Buick-A-Dealer-For-The-People-dealer-reviews-23685 |
| DealerRater__Rank      | Rank Quantity        | 3                                                                   |


## Unit tests

It is possible to run unit tests using Docker

```
docker run --rm -v ${pwd}:/app -w /app/tests/Scrapper.Application.Tests mcr.microsoft.com/dotnet/sdk:6.0 dotnet test --logger:trx
```


