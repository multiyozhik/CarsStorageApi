
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CarsStorageApi/CarsStorageApi.csproj", "CarsStorageApi/"]
COPY ["../CarsStorage.BLL/CarsStorage.BLL.Services.csproj", "CarsStorage.BLL/"]
COPY ["../CarsStorage.Abstractions/CarsStorage.Abstractions.csproj", "CarsStorage.Abstractions/"]
COPY ["../CarsStorage.DAL/CarsStorage.DAL.csproj", "CarsStorage.DAL/"]
COPY ["../CarsStorage.DAL.Repositories/CarsStorage.DAL.Repositories.csproj", "CarsStorage.DAL.Repositories/"]
RUN dotnet restore "CarsStorageApi/CarsStorageApi.csproj"

COPY . .
WORKDIR "/src/CarsStorageApi"
RUN dotnet build "./CarsStorageApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CarsStorageApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

COPY CarsStorageApi/appsettings.json /app/appsettings.json

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY CarsStorageApi/appsettings.json /app/appsettings.json

RUN mkdir -p /app/Certificates
COPY ../Certificates/aspnetapp.pfx /app/Certificates/
RUN chmod 644 /app/Certificates/aspnetapp.pfx

ENTRYPOINT ["dotnet", "CarsStorageApi.dll"]