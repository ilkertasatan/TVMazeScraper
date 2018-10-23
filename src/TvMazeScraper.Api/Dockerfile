FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5000

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["src/TvMazeScraper.Api/TvMazeScraper.Api.csproj", "src/TvMazeScraper.Api/"]
COPY ["src/TvMazeScraper.Infrastructure/TvMazeScraper.Infrastructure.csproj", "src/TvMazeScraper.Infrastructure/"]
COPY ["src/TvMazeScraper.Domain/TvMazeScraper.Domain.csproj", "src/TvMazeScraper.Domain/"]
COPY ["src/TvMazeScraper.Domain.Anticorruption/TvMazeScraper.Domain.Anticorruption.csproj", "src/TvMazeScraper.Domain.Anticorruption/"]
RUN dotnet restore "src/TvMazeScraper.Api/TvMazeScraper.Api.csproj"
COPY . .
WORKDIR "/src/src/TvMazeScraper.Api"
RUN dotnet build "TvMazeScraper.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TvMazeScraper.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TvMazeScraper.Api.dll"]