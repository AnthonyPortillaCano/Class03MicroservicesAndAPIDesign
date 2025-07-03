using MediatR;
using OrderService.Data;
using OrderService.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using Shared;
using System.Text.Json;
using MassTransit;

namespace OrderService.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly OrderDbContext _context;
        private readonly IBus _bus; // MassTransit bus for publishing events to RabbitMQ
        public CreateOrderCommandHandler(OrderDbContext context, IBus bus)
        {
            _context = context;
            _bus = bus;
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerName = request.CustomerName,
                Product = request.Product,
                Quantity = request.Quantity,
                CreatedAt = DateTime.UtcNow
            };
            _context.Orders.Add(order);

            // Crear el evento y serializarlo
            var orderCreatedEvent = new OrderCreatedEvent
            {
                OrderId = order.Id,
                UserId = request.UserId,
                Amount = request.Amount,
                CustomerName = order.CustomerName,
                Product = order.Product,
                Quantity = order.Quantity,
                CreatedAt = order.CreatedAt
            };
            var outboxEvent = new OutboxEvent
            {
                Id = Guid.NewGuid(),
                EventType = nameof(OrderCreatedEvent),
                Payload = JsonSerializer.Serialize(orderCreatedEvent),
                OccurredOn = DateTime.UtcNow,
                Published = false
            };
            _context.OutboxEvents.Add(outboxEvent);

            await _context.SaveChangesAsync(cancellationToken);

            // Publish the event to RabbitMQ using MassTransit
            await _bus.Publish(orderCreatedEvent);

            return order.Id;
        }
    }
} 