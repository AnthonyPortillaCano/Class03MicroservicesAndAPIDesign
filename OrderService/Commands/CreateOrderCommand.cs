using MediatR;
using System;

namespace OrderService.Commands
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public string CustomerName { get; set; } = string.Empty;
        public string Product { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
} 