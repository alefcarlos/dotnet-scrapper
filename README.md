# dotnet-scrapper

It scrappes reviews from `www.dealerrater.com` and evaluates reviews ranked by Username and sum of POSITIVE ratings.
## How to run

```
docker compose up
```

### Configurations

It is possible to configure application using envs:

| Name                   | Description          | Default                                                                                                 |
| ---------------------- | -------------------- | ------------------------------------------------------------------------------------------------------- |
| DealerRater__PageCount | Total pages to fetch | 5                                                                                                       |
| DealerRater__DealerUrl | Dealer url           | https://www.dealerrater.com/dealer/McKaig-Chevrolet-Buick-A-Dealer-For-The-People-dealer-reviews-23685/ |
