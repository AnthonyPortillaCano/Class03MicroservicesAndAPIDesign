using MediatR;
using System;

namespace OrderService.Commands
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string Product { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
} 