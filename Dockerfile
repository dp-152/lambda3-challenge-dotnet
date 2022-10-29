# Restore and build
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /app

COPY *.sln .
COPY docker-compose.dcproj ./
COPY CopaGames.Tests/*.csproj ./CopaGames.Tests/
COPY CopaGames.Domain/*.csproj ./CopaGames.Domain/
COPY CopaGames.Infrastructure/*.csproj ./CopaGames.Infrastructure/
COPY CopaGames.API/*.csproj ./CopaGames.API/
COPY CopaGames.Application/*.csproj ./CopaGames.Application/

RUN dotnet restore CopaGames.sln

COPY . .

RUN dotnet build CopaGames.sln

# Run tests
FROM build AS test

WORKDIR /app

RUN dotnet test --logger:trx

# Publish app
FROM build AS publish

WORKDIR /app/CopaGames.API
RUN dotnet publish -c Release -o out

# Run app
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS runtime
WORKDIR /app
COPY --from=publish /app/CopaGames.API/out ./

EXPOSE 80
ENTRYPOINT [ "dotnet", "CopaGames.API.dll" ]
