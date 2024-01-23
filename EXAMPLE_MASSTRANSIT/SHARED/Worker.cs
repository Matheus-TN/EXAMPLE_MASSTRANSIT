using MassTransit;
using Microsoft.Extensions.Hosting;
using SHARED.Models;

namespace SHARED
{
    public class Worker : BackgroundService
    {
        readonly IBus _bus;

        public Worker(IBus bus)
        {
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _bus.Publish(new HelloMessage
                {
                    Message = "Hello"
                }, stoppingToken);

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
