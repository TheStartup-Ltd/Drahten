﻿
namespace UserService.Application.Commands
{
    public record AddContactRequestCommand(Guid IssuerUserId, Guid ReceiverUserId, 
        DateTimeOffset DateTime, string Message = null) : ICommand;
}
