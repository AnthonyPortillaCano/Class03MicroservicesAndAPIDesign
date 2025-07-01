using MediatR;
using OrderService.Models;
using System;

namespace OrderService.Queries
{
    public class GetOrderByIdQuery : IRequest<Order?>
    {
        public Guid Id { get; set; }
        public GetOrderByIdQuery(Guid id) => Id = id;
    }
} 