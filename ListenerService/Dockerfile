
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["ListenerService/ListenerService.csproj", "ListenerService/"]
RUN dotnet restore "./ListenerService/ListenerService.csproj"
COPY . .
WORKDIR "/src/ListenerService"
RUN dotnet build "./ListenerService.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ListenerService.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ListenerService/appsettings.json /app/appsettings.json

ENTRYPOINT ["dotnet", "ListenerService.dll"]