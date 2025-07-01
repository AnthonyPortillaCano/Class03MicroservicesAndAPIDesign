using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderService.Data;
using OrderService.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.Services
{
    public class OutboxPublisherService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHttpClientFactory _httpClientFactory;
        private const string NotificationServiceUrl = "http://localhost:6014/api/notifications/from-event";

        public OutboxPublisherService(IServiceProvider serviceProvider, IHttpClientFactory httpClientFactory)
        {
            _serviceProvider = serviceProvider;
            _httpClientFactory = httpClientFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
                    var pendingEvents = await db.OutboxEvents
                        .Where(e => !e.Published)
                        .OrderBy(e => e.OccurredOn)
                        .ToListAsync(stoppingToken);

                    foreach (var evt in pendingEvents)
                    {
                        try
                        {
                            var client = _httpClientFactory.CreateClient();
                            var content = new StringContent(evt.Payload, Encoding.UTF8, "application/json");
                            var response = await client.PostAsync(NotificationServiceUrl, content, stoppingToken);
                            if (response.IsSuccessStatusCode)
                            {
                                evt.Published = true;
                                evt.PublishedOn = DateTime.UtcNow;
                                await db.SaveChangesAsync(stoppingToken);
                            }
                        }
                        catch
                        {
                            // Log o ignorar para reintentar en el siguiente ciclo
                        }
                    }
                }
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
    }
} 