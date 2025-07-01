using MediatR;
using OrderService.Data;
using OrderService.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OrderService.Queries
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Order?>
    {
        private readonly OrderDbContext _context;
        public GetOrderByIdQueryHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Order?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);
        }
    }
} 