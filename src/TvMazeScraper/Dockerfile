FROM microsoft/dotnet:sdk AS base
WORKDIR /app

FROM microsoft/dotnet:sdk AS build
WORKDIR /src
COPY ["src/TvMazeScraper.Domain/TvMazeScraper.Domain.csproj", "src/TvMazeScraper.Domain/"]
COPY ["src/TvMazeScraper.Domain.Anticorruption/TvMazeScraper.Domain.Anticorruption.csproj", "src/TvMazeScraper.Domain.Anticorruption/"]
#RUN dotnet restore "src/TvMazeScraper/TvMazeScraper.csproj"

COPY . .
WORKDIR "/src/src/TvMazeScraper"
RUN dotnet build "TvMazeScraper.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TvMazeScraper.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TvMazeScraper.dll"]