using Microsoft.EntityFrameworkCore;
using WarehouseInventory.Application.Commands.Handlers;
using WarehouseInventory.Infrastructure;
using WarehouseInventory.Application.Queries.Handlers;
using WarehouseInventory.Infrastructure.Repositories;
using WarehouseInventory.Domain.Repositories;

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

// Register Handlers
builder.Services.AddScoped<AddInventoryItemHandler>();
builder.Services.AddScoped<GetInventoryItemHandler>();
builder.Services.AddScoped<GetAllInventoryItemsHandler>();
builder.Services.AddScoped<RegisterIngoingStockHandler>();
builder.Services.AddScoped<RegisterOutgoingStockHandler>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
