using MediatR;
using System;

namespace NotificationService.Commands
{
    public class DeleteNotificationCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public DeleteNotificationCommand(Guid id) => Id = id;
    }
} 