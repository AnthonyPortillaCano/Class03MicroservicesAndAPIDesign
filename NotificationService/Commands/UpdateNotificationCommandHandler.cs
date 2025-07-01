using MediatR;
using NotificationService.Data;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NotificationService.Commands
{
    public class UpdateNotificationCommandHandler : IRequestHandler<UpdateNotificationCommand, bool>
    {
        private readonly NotificationDbContext _context;
        public UpdateNotificationCommandHandler(NotificationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = await _context.Notifications.FirstOrDefaultAsync(n => n.Id == request.Id, cancellationToken);
            if (notification == null) return false;
            notification.Message = request.Message;
            notification.IsRead = request.IsRead;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
} 