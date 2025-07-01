using System;

namespace OrderService.Models
{
    public class OutboxEvent
    {
        public Guid Id { get; set; }
        public string EventType { get; set; } = string.Empty;
        public string Payload { get; set; } = string.Empty;
        public DateTime OccurredOn { get; set; }
        public bool Published { get; set; }
        public DateTime? PublishedOn { get; set; }
    }
} 