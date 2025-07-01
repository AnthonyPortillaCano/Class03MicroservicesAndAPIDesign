using MediatR;
using System;

namespace NotificationService.Commands
{
    public class CreateNotificationCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public string Message { get; set; } = string.Empty;
    }
} 