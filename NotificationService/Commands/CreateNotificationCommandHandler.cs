using MediatR;
using NotificationService.Data;
using NotificationService.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationService.Commands
{
    public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, Guid>
    {
        private readonly NotificationDbContext _context;
        public CreateNotificationCommandHandler(NotificationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = new Notification
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                Message = request.Message,
                CreatedAt = DateTime.UtcNow,
                IsRead = false
            };
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync(cancellationToken);
            return notification.Id;
        }
    }
} 