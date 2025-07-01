using MediatR;
using NotificationService.Data;
using NotificationService.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NotificationService.Queries
{
    public class GetNotificationsQueryHandler : IRequestHandler<GetNotificationsQuery, IEnumerable<Notification>>
    {
        private readonly NotificationDbContext _context;
        public GetNotificationsQueryHandler(NotificationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Notification>> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Notifications.ToListAsync(cancellationToken);
        }
    }
} 