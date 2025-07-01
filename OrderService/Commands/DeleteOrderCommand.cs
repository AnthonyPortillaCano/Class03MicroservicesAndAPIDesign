using MediatR;
using System;

namespace OrderService.Commands
{
    public class DeleteOrderCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public DeleteOrderCommand(Guid id) => Id = id;
    }
} 