using MediatR;
using OrderService.Data;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OrderService.Commands
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, bool>
    {
        private readonly OrderDbContext _context;
        public UpdateOrderCommandHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);
            if (order == null) return false;
            order.CustomerName = request.CustomerName;
            order.Product = request.Product;
            order.Quantity = request.Quantity;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
} 