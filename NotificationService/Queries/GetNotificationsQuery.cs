using MediatR;
using NotificationService.Models;
using System.Collections.Generic;

namespace NotificationService.Queries
{
    public class GetNotificationsQuery : IRequest<IEnumerable<Notification>>
    {
    }
} 