FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

COPY *.sln .
COPY src/Wallet.Domain/Wallet.Domain.csproj ./src/Wallet.Domain/
COPY src/Wallet.Persistence/Wallet.Persistence.csproj ./src/Wallet.Persistence/
COPY src/Wallet.Api/Wallet.Api.csproj ./src/Wallet.Api/
COPY src/Wallet.Application/Wallet.Application.csproj ./src/Wallet.Application/
RUN dotnet restore

COPY src/. ./src/
WORKDIR /source/src
RUN dotnet publish Wallet.Api -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "Wallet.Api.dll"]