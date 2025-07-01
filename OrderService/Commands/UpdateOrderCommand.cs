using MediatR;
using System;

namespace OrderService.Commands
{
    public class UpdateOrderCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string Product { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
} 