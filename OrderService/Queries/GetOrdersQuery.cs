using MediatR;
using OrderService.Models;
using System.Collections.Generic;

namespace OrderService.Queries
{
    public class GetOrdersQuery : IRequest<IEnumerable<Order>>
    {
    }
} 