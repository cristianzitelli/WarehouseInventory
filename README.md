# WarehouseInventory
CQRS Training
A sample .NET 8 Web API implementing CQRS. Includes Swagger for API exploration.

## Contents

WarehouseInventory.Api/ — ASP.NET Core Web API (exposes endpoints and Swagger)
WarehouseInventory.Application/ — Command and Queries handlers
WarehouseInventory.Domain/ — Domain model
WarehouseInventory.Infrastructure/ — EF Core DbContext and entity mappings
db/init/ — SQL scripts to create tables and seed data
docker-compose.yml — Postgres + API services
Dockerfile — Multi-stage Dockerfile for the API

## Requirements

.NET 8 SDK
Docker & Docker Compose (or Docker Desktop)
Optional: Visual Studio 2022 with Container Tools
Quick start (Docker Compose)

From repository root, build and start containers:
docker-compose up --build

Wait until both containers are healthy. Check status:
docker-compose ps

Open Swagger UI:
http://localhost:5000/swagger

## Example endpoints:

Create item (command): POST /api/inventory/items
Register ingoing stock (command): POST /api/inventory/items/{sku}/register-ingoing-stock
Register outgoing stock (command): POST /api/inventory/items/{sku}/register-outgoing-stock
Get item by SKU (query): GET /api/inventory/items/{sku}
Get items: GET /api/inventory/items
Local development (without Docker)

Set the DefaultConnection in appsettings.json to your Postgres connection or use local DB.

Run the API:
cd WarehouseInventory.Api
dotnet run

## Configuration

Connection string key: ConnectionStrings:DefaultConnection
Docker Compose notes

db/init SQL scripts run only on first initialization of the Postgres volume. To re-run, remove the volume:
docker-compose down -v
docker-compose up --build

To run with Visual Studio 2022:

Add Container Orchestrator Support (or open docker-compose project).
Set docker-compose as startup project and press F5.