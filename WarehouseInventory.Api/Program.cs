using Microsoft.EntityFrameworkCore;
using WarehouseInventory.Api.Workers;
using WarehouseInventory.Application.Commands;
using WarehouseInventory.Application.Commands.Handlers;
using WarehouseInventory.Application.Dispatchers;
using WarehouseInventory.Application.Handlers;
using WarehouseInventory.Application.Queries;
using WarehouseInventory.Application.Queries.Handlers;
using WarehouseInventory.Domain.Aggregates;
using WarehouseInventory.Domain.Events;
using WarehouseInventory.Domain.Repositories;
using WarehouseInventory.Infrastructure;
using WarehouseInventory.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
var conn = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseNpgsql(conn, npgsqlOptions =>
    {
        npgsqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
    }));

//We could have another db here
builder.Services.AddDbContext<InventoryReadDbContext>(options =>
    options.UseNpgsql(conn, npgsqlOptions =>
    {
        npgsqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
    }));

// Register Handlers
builder.Services.AddScoped<ICommandHandler<AddInventoryItemCommand>,AddInventoryItemHandler>();
builder.Services.AddScoped<IQueryHandler<GetInventoryItemQuery, InventoryItem?>, GetInventoryItemHandler>();
builder.Services.AddScoped<IQueryHandler<GetAllInventoryItemsQuery, IEnumerable<InventoryItem>>, GetAllInventoryItemsHandler>();
builder.Services.AddScoped<IQueryHandler<GetLowStockInventoryItemsQuery, IEnumerable<InventoryItem>>, GetLowStockInventoryItemsHandler>();
builder.Services.AddScoped<IQueryHandler<GetStockMovementsQuery, IEnumerable<StockMovement>>, GetStockMovementsHandler>();
builder.Services.AddScoped<ICommandHandler<RegisterIngoingStockCommand>, RegisterIngoingStockHandler>();
builder.Services.AddScoped<ICommandHandler<RegisterOutgoingStockCommand>, RegisterOutgoingStockHandler>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IInventoryReadRepository, InventoryReadRepository>();
builder.Services.AddScoped<IStockMovementReadRepository, StockMovementReadRepository>();
builder.Services.AddScoped<IOutboxRepository, OutboxRepository>();
builder.Services.AddScoped<IEventHandler<InventoryItemCreated>, InventoryItemCreatedReadHandler>();
builder.Services.AddScoped<IEventHandler<StockAdded>, StockAddedReadHandler>();
builder.Services.AddScoped<IEventHandler<StockRemoved>, StockRemovedReadHandler>();
builder.Services.AddScoped<IEventDispatcher, EventDispatcher>();

builder.Services.AddHostedService<OutboxDispatcher>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
