FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENV DOTNET_TELEMETRY_OPTOUT=1

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["WarehouseInventory.Api/WarehouseInventory.Api.csproj", "WarehouseInventory.Api/"]
COPY ["WarehouseInventory.Infrastructure/WarehouseInventory.Infrastructure.csproj", "WarehouseInventory.Infrastructure/"]
COPY ["WarehouseInventory.Application/WarehouseInventory.Application.csproj", "WarehouseInventory.Application/"]
COPY ["WarehouseInventory.Domain/WarehouseInventory.Domain.csproj", "WarehouseInventory.Domain/"]

RUN dotnet restore "WarehouseInventory.Api/WarehouseInventory.Api.csproj"

# Copy all source files
COPY . .

WORKDIR "/src/WarehouseInventory.Api"
RUN dotnet build "WarehouseInventory.Api.csproj" -c Release -o /app/build

# Publish the API
FROM build AS publish
RUN dotnet publish "WarehouseInventory.Api.csproj" -c Release -o /app/publish /p:TrimUnusedDependencies=true

FROM base AS final
WORKDIR /app

# Copy the published files from build stage
COPY --from=publish /app/publish .

# Set the entrypoint
ENTRYPOINT ["dotnet", "WarehouseInventory.Api.dll"]