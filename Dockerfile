#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CarsStorageApi/CarsStorageApi.csproj", "CarsStorageApi/"]
COPY ["CarsStorage.BLL/CarsStorage.BLL.Services.csproj", "CarsStorage.BLL/"]
COPY ["CarsStorage.Abstractions/CarsStorage.Abstractions.csproj", "CarsStorage.Abstractions/"]
COPY ["CarsStorage.DAL/CarsStorage.DAL.csproj", "CarsStorage.DAL/"]
COPY ["CarsStorage.DAL.Repositories/CarsStorage.DAL.Repositories.csproj", "CarsStorage.DAL.Repositories/"]
RUN dotnet restore "CarsStorageApi/CarsStorageApi.csproj"
COPY . .
WORKDIR "/src/CarsStorageApi"
RUN dotnet build "./CarsStorageApi.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CarsStorageApi.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

COPY CarsStorageApi/appsettings.json /app/appsettings.json

#для исп. при запуске из VS
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY CarsStorageApi/appsettings.json /app/appsettings.json

ENTRYPOINT ["dotnet", "CarsStorageApi.dll"]