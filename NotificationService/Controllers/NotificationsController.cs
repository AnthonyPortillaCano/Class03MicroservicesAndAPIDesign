using MediatR;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Commands;
using NotificationService.Models;
using NotificationService.Queries;
using Shared;

namespace NotificationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public NotificationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotifications()
        {
            var result = await _mediator.Send(new GetNotificationsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Notification>> GetNotification(Guid id)
        {
            var notification = await _mediator.Send(new GetNotificationByIdQuery(id));
            if (notification == null) return NotFound();
            return Ok(notification);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateNotification([FromBody] CreateNotificationCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetNotification), new { id }, command);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNotification(Guid id, [FromBody] UpdateNotificationCommand command)
        {
            if (id != command.Id) return BadRequest();
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(Guid id)
        {
            var result = await _mediator.Send(new DeleteNotificationCommand(id));
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpPost("from-event")]
        public async Task<IActionResult> ReceiveOrderCreatedEvent([FromBody] OrderCreatedEvent evt)
        {
            // Create Notification by the event
            var command = new CreateNotificationCommand
            {
                UserId = Guid.NewGuid(),
                Message = $"New order created: {evt.Product} x{evt.Quantity} para {evt.CustomerName}",
            };
            await _mediator.Send(command);
            return Ok();
        }
    }
} 