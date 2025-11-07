using Microsoft.AspNetCore.Mvc;
using WarehouseInventory.Application.Commands;
using WarehouseInventory.Application.Handlers;
using WarehouseInventory.Application.Queries;
using WarehouseInventory.Domain.Aggregates;
using WarehouseInventory.Models;

namespace WarehouseInventory.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly ILogger<InventoryController> _logger;

    public InventoryController(ILogger<InventoryController> logger)
    {
        _logger = logger;
    }

    [HttpGet("items")]
    public async Task<IActionResult> GetItems([FromServices] IQueryHandler<GetAllInventoryItemsQuery, IEnumerable<InventoryItem>> handler)
    {
        var query = new GetAllInventoryItemsQuery();
        var items = (await handler.HandleAsync(query)).Select(i => new InventoryItemDto { Sku = i.Sku, Name = i.Name, Quantity = i.Quantity });
        return Ok(items);
    }

    [HttpGet("items/low-stock")]
    public async Task<IActionResult> GetItemsLowStock([FromServices] IQueryHandler<GetLowStockInventoryItemsQuery, IEnumerable<InventoryItem>> handler)
    {
        var query = new GetLowStockInventoryItemsQuery();
        var items = (await handler.HandleAsync(query)).Select(i => new InventoryItemDto { Sku = i.Sku, Name = i.Name, Quantity = i.Quantity });
        return Ok(items);
    }

    [HttpGet("items/logs")]
    public async Task<IActionResult> GetMovements([FromServices] IQueryHandler<GetStockMovementsQuery, IEnumerable<StockMovement>> handler)
    {
        var query = new GetStockMovementsQuery();
        var items = (await handler.HandleAsync(query)).Select(i => new StockMovementDto { Details = i.ToString() });
        return Ok(items);
    }

    [HttpGet("items/{sku}")]
    public async Task<IActionResult> GetItems([FromServices] IQueryHandler<GetInventoryItemQuery, InventoryItem?> handler, string sku)
    {
        var query = new GetInventoryItemQuery(sku);
        var item = await handler.HandleAsync(query);
        if (item is null)
            return NotFound();

        return Ok(new InventoryItemDto { Sku = item.Sku, Name = item.Name, Quantity = item.Quantity });
    }

    [HttpPost("items")]
    public async Task<IActionResult> Post([FromServices] ICommandHandler<AddInventoryItemCommand> handler, NewInventoryItemDto dto)
    {
        var command = new AddInventoryItemCommand(dto.Sku, dto.Name);
        await handler.HandleAsync(command);
        return Ok();
    }

    [HttpPost("items/{sku}/register-ingoing-stock")]
    public async Task<IActionResult> RegisterIngoing([FromServices] ICommandHandler<RegisterIngoingStockCommand> handler, string sku, NewStockMovementDto dto)
    {
        var command = new RegisterIngoingStockCommand(sku, dto.Quantity);
        await handler.HandleAsync(command);
        return Ok();
    }

    [HttpPost("items/{sku}/register-outgoing-stock")]
    public async Task<IActionResult> RegisterOutgoing([FromServices] ICommandHandler<RegisterOutgoingStockCommand> handler, string sku, NewStockMovementDto dto)
    {
        var command = new RegisterOutgoingStockCommand(sku, dto.Quantity);
        await handler.HandleAsync(command);
        return Ok();
    }
}
