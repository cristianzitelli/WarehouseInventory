using Microsoft.AspNetCore.Mvc;
using WarehouseInventory.Application.Commands;
using WarehouseInventory.Application.Commands.Handlers;
using WarehouseInventory.Models;
using WarehouseInventory.Application.Queries;
using WarehouseInventory.Application.Queries.Handlers;

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
    public async Task<IActionResult> GetItems([FromServices] GetAllInventoryItemsHandler handler)
    {
        var query = new GetAllInventoryItemsQuery();
        var items = (await handler.HandleAsync(query)).Select(i => new InventoryItemDto{ Sku = i.Sku, Name = i.Name, Quantity = i.Quantity });
        return Ok(items);
    }

    [HttpGet("items/{sku}")]
    public async Task<IActionResult> GetItems([FromServices] GetInventoryItemHandler handler, string sku)
    {
        var query = new GetInventoryItemQuery(sku);
        var item = await handler.HandleAsync(query);
        if (item is null)
            return NotFound();

        return Ok(new InventoryItemDto { Sku = item.Sku, Name = item.Name, Quantity = item.Quantity });
    }

    [HttpPost("items")]
    public async Task<IActionResult> Post([FromServices] AddInventoryItemHandler handler, NewInventoryItemDto dto)
    {
        var command = new AddInventoryItemCommand(dto.Sku, dto.Name);
        var sku = await handler.HandleAsync(command);
        return Ok(sku);
    }

    [HttpPost("items/{sku}/register-ingoing-stock")]
    public async Task<IActionResult> RegisterIngoing([FromServices] RegisterIngoingStockHandler handler, string sku, StockMovementDto dto)
    {
        var command = new RegisterIngoingStockCommand(sku, dto.Quantity);
        await handler.HandleAsync(command);
        return Ok();
    }

    [HttpPost("items/{sku}/register-outgoing-stock")]
    public async Task<IActionResult> RegisterOutgoing([FromServices] RegisterOutgoingStockHandler handler, string sku, StockMovementDto dto)
    {
        var command = new RegisterOutgoingStockCommand(sku, dto.Quantity);
        await handler.HandleAsync(command);
        return Ok();
    }
}
