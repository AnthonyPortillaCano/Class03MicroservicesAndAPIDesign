namespace NotificationService.Events
{
    public class OrderCreatedEvent
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
} 