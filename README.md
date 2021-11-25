# dotnet-scrapper [![build](https://github.com/alefcarlos/dotnet-scrapper/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/alefcarlos/dotnet-scrapper/actions/workflows/ci.yml)

## Get started

It scrappes `N` review pages for an specific dealer and evaluates them

### Evaluate logic

```
1. Select all rating values equal to  5
2. Sum all detailed ratings
3. Calculate mean of that sum
5. Filter candidates that would be a fake review
```

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


