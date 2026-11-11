
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY NovaHarvest.Web/*.csproj ./NovaHarvest.Web/

RUN dotnet restore ./NovaHarvest.Web/NovaHarvest.Web.csproj

COPY NovaHarvest.Web/ ./NovaHarvest.Web/

WORKDIR /src/NovaHarvest.Web

RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080

EXPOSE 8080

ENTRYPOINT ["dotnet", "NovaHarvest.Web.dll"]
