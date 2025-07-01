using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Models;
using MediatR;
using OrderService.Commands;
using OrderService.Queries;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly OrderDbContext _context;
        private readonly IMediator _mediator;
        public OrdersController(OrderDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var result = await _mediator.Send(new GetOrdersQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(Guid id)
        {
            var order = await _mediator.Send(new GetOrderByIdQuery(id));
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetOrder), new { id }, command);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] UpdateOrderCommand command)
        {
            if (id != command.Id) return BadRequest();
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var result = await _mediator.Send(new DeleteOrderCommand(id));
            if (!result) return NotFound();
            return NoContent();
        }
    }
} 