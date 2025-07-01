using System;
using System.ComponentModel.DataAnnotations;

namespace OrderService.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string Product { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
} 