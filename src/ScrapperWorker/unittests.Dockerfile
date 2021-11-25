FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /tests
COPY ["tests/Scrapper.Application.Tests/Scrapper.Application.Tests.csproj", "tests/Scrapper.Application.Tests/"]
RUN dotnet restore "tests/Scrapper.Application.Tests/Scrapper.Application.Tests.csproj"
COPY . .
WORKDIR "/tests/tests/Scrapper.Application.Tests"
RUN dotnet build "Scrapper.Application.Tests.csproj" -c Release

FROM build AS testrunner
WORKDIR "/tests/tests/Scrapper.Application.Tests"

CMD ["dotnet", "test", "--no-restore"]