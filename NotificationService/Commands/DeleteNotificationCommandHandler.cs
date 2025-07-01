using MediatR;
using NotificationService.Data;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NotificationService.Commands
{
    public class DeleteNotificationCommandHandler : IRequestHandler<DeleteNotificationCommand, bool>
    {
        private readonly NotificationDbContext _context;
        public DeleteNotificationCommandHandler(NotificationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = await _context.Notifications.FirstOrDefaultAsync(n => n.Id == request.Id, cancellationToken);
            if (notification == null) return false;
            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
} 