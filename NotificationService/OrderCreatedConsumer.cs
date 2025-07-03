using MassTransit;
using Shared;
using NotificationService.Data;
using NotificationService.Models;

// Consumer for OrderCreatedEvent, triggered by RabbitMQ messages
public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
{
    private readonly NotificationDbContext _db;
    public OrderCreatedConsumer(NotificationDbContext db)
    {
        _db = db;
    }

    // This method is called automatically when a new OrderCreatedEvent is received from RabbitMQ
    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var evt = context.Message;
        // Create and save a notification in the database based on the event
        var notification = new Notification
        {
            Id = Guid.NewGuid(),
            UserId = Guid.NewGuid(), // If you have a real mapping, use evt.UserId
            Message = $"New order created: OrderId={evt.OrderId}, Amount={evt.Amount}",
            CreatedAt = evt.CreatedAt,
            IsRead = false
        };
        _db.Notifications.Add(notification);
        await _db.SaveChangesAsync();
        Console.WriteLine($"[NotificationService] Notification saved for OrderId={evt.OrderId}");
    }
} 