using System;

namespace Shared
{
    public class OrderCreatedEvent
    {
        public Guid OrderId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string Product { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
    }
} 