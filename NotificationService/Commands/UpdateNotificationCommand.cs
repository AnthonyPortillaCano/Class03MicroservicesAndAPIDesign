using MediatR;
using System;

namespace NotificationService.Commands
{
    public class UpdateNotificationCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsRead { get; set; }
    }
} 