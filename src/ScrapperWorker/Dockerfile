#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/ScrapperWorker/ScrapperWorker.csproj", "src/ScrapperWorker/"]
RUN dotnet restore "src/ScrapperWorker/ScrapperWorker.csproj"
COPY . .
WORKDIR "/src/src/ScrapperWorker"
RUN dotnet build "ScrapperWorker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ScrapperWorker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ScrapperWorker.dll"]