# dotnet-scrapper [![build](https://github.com/alefcarlos/dotnet-scrapper/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/alefcarlos/dotnet-scrapper/actions/workflows/ci.yml) [![codecov](https://codecov.io/gh/alefcarlos/dotnet-scrapper/branch/main/graph/badge.svg?token=lWcKCpZp5F)](https://codecov.io/gh/alefcarlos/dotnet-scrapper)

## Get started

It scrappes `N` review pages for an specific dealer and evaluates them

### Evaluate logic

```
1. Select all rating values equal to  5
2. Sum all detailed ratings
3. Calculate mean of that sum
4. Filter candidates that would be a fake review using 5 as a threshold
```

### How to run

```
docker compose up
```
#### Configurations

It is possible to configure Scrapper using these envs:

| Name                   | Description          | Default                                                             |
| ---------------------- | -------------------- | ------------------------------------------------------------------- |
| DealerRater__PageCount | Total pages to fetch | 5                                                                   |
| DealerRater__BaseUrl   | Base url             | https://www.dealerrater.com/dealer/                                 |
| DealerRater__Dealer    | Dealer url           | McKaig-Chevrolet-Buick-A-Dealer-For-The-People-dealer-reviews-23685 |
| DealerRater__Rank      | Rank Quantity        | 3                                                                   |
| DealerRater__Threshold | Rank Threshold       | 5                                                                   |


## Unit tests

It is possible to run unit tests using Docker

Linux

```
docker run --rm -v $(pwd):/app -w /app/tests/Scrapper.Application.Tests mcr.microsoft.com/dotnet/sdk:6.0 dotnet test --logger:trx
```

Windows Powershell

```
docker run --rm -v ${pwd}:/app -w /app/tests/Scrapper.Application.Tests mcr.microsoft.com/dotnet/sdk:6.0 dotnet test --logger:trx
```

## Example

[![asciicast](https://asciinema.org/a/fUD4B3PiIPzpXtLrLd3sJm0vx.svg)](https://asciinema.org/a/fUD4B3PiIPzpXtLrLd3sJm0vx)
