using MediatR;
using NotificationService.Models;
using System;

namespace NotificationService.Queries
{
    public class GetNotificationByIdQuery : IRequest<Notification?>
    {
        public Guid Id { get; set; }
        public GetNotificationByIdQuery(Guid id) => Id = id;
    }
} 