using MediatR;
using NotificationService.Data;
using NotificationService.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NotificationService.Queries
{
    public class GetNotificationByIdQueryHandler : IRequestHandler<GetNotificationByIdQuery, Notification?>
    {
        private readonly NotificationDbContext _context;
        public GetNotificationByIdQueryHandler(NotificationDbContext context)
        {
            _context = context;
        }

        public async Task<Notification?> Handle(GetNotificationByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Notifications.FirstOrDefaultAsync(n => n.Id == request.Id, cancellationToken);
        }
    }
} 