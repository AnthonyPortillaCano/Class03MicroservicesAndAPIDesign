using MediatR;
using OrderService.Data;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OrderService.Commands
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
    {
        private readonly OrderDbContext _context;
        public DeleteOrderCommandHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);
            if (order == null) return false;
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
} 