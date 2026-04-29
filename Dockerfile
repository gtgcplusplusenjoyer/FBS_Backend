FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["src/FBS.API/FBS.API/FBS.API.csproj", "FBS.API/"]
COPY ["src/FBS.API/FBS.Application/FBS.Application.csproj", "FBS.Application/"]
COPY ["src/FBS.API/FBS.Core/FBS.Core.csproj", "FBS.Core/"]
COPY ["src/FBS.API/FBS.Infrastructure/FBS.Infrastructure.csproj", "FBS.Infrastructure/"]

RUN dotnet restore "FBS.API/FBS.API.csproj"

COPY . .

# Копируем appsettings
COPY ["src/FBS.API/FBS.API/appsettings.json", "appsettings.json"]
COPY ["src/FBS.API/FBS.API/appsettings.Development.json", "appsettings.Development.json"]

WORKDIR /src
RUN dotnet publish "src/FBS.API/FBS.API/FBS.API.csproj" -c Release -o /publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
EXPOSE 5067
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "FBS.API.dll"]