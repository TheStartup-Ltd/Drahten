﻿using PrivateHistoryService.Application.Exceptions;
using PrivateHistoryService.Domain.Repositories;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Application.Commands.Handlers
{
    internal sealed class RemoveSearchedTopicDataHandler : ICommandHandler<RemoveSearchedTopicDataCommand>
    {
        private readonly IUserRepository _userRepository;

        public RemoveSearchedTopicDataHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task HandleAsync(RemoveSearchedTopicDataCommand command)
        {
            var user = await _userRepository.GetUserByIdAsync(command.UserId);

            if (user == null)
            {
                throw new UserNotFoundException(command.UserId);
            }

            var searchedTopicData = new SearchedTopicData(command.TopicId, command.UserId, command.SearchedData, command.DateTime);

            user.RemoveSearchedTopicData(searchedTopicData);

            await _userRepository.UpdateUserAsync(user);
        }
    }
}
