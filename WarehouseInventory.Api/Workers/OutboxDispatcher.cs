using WarehouseInventory.Application.Dispatchers;
using WarehouseInventory.Domain.Repositories;

namespace WarehouseInventory.Api.Workers
{
    public class OutboxDispatcher : BackgroundService
    {
        private readonly ILogger<OutboxDispatcher> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        public OutboxDispatcher(IServiceScopeFactory scopeFactory, ILogger<OutboxDispatcher> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) 
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var msgs = await scope.ServiceProvider.GetRequiredService<IOutboxRepository>().GetUnprocessedAsync(stoppingToken);
                foreach (var msg in msgs)
                {
                    try
                    {
                        await scope.ServiceProvider.GetRequiredService<IEventDispatcher>().DispatchAsync(msg, stoppingToken);

                        await scope.ServiceProvider.GetRequiredService<IOutboxRepository>().MarkProcessed(msg.Id);
                    }
                    catch (Exception e)
                    {
                        _logger.LogError($"Error processing events, Message: {e.Message}");
                    }
                }
                await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
            }
        }

    }
}
