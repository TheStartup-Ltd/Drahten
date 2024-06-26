﻿using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PrivateHistoryService.Application.Dtos;
using PrivateHistoryService.Application.Services.ReadServices;
using PrivateHistoryService.Application.Services.WriteServices;
using PrivateHistoryService.Domain.Factories.Interfaces;
using PrivateHistoryService.Domain.Repositories;
using PrivateHistoryService.Domain.ValueObjects;
using PrivateHistoryService.Infrastructure.Dtos;
using PrivateHistoryService.Infrastructure.Schedulers;
using System.Text.Json;
using static System.Formats.Asn1.AsnWriter;


namespace PrivateHistoryService.Infrastructure.EventProcessing
{
    internal enum EventType
    {
        ViewedArticle,
        LikedArticle,
        DislikedArticle,
        CommentedArticle,
        LikedArticleComment,
        DislikedArticleComment,
        TopicSubscription,
        SearchedArticleData,
        Undetermined
    }

    public sealed class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMapper _mapper;
        private readonly ILogger<EventProcessor> _logger;

        public EventProcessor(IServiceScopeFactory serviceScopeFactory, IMapper mapper, ILogger<EventProcessor> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _mapper = mapper;
            _logger = logger;
        }

        private EventType DetermineEvent(string notificationMessage)
        {
            _logger.LogInformation("PrivateHistoryService --> EventProcessor: Determining Event Type.");

            var eventType = JsonSerializer.Deserialize<MessageBusEventDto>(notificationMessage);

            switch (eventType.Event)
            {
                case "ViewedArticle":
                    _logger.LogInformation("PrivateHistoryService --> EventProcessor: ViewedArticle event detected!");
                    return EventType.ViewedArticle;
                case "LikedArticle":
                    _logger.LogInformation("PrivateHistoryService --> EventProcessor: LikedArticle event detected!");
                    return EventType.LikedArticle;
                case "DislikedArticle":
                    _logger.LogInformation("PrivateHistoryService --> EventProcessor: DislikedArticle event detected!");
                    return EventType.DislikedArticle;
                case "CommentedArticle":
                    _logger.LogInformation("PrivateHistoryService --> EventProcessor: CommentedArticle event detected!");
                    return EventType.CommentedArticle;
                case "LikedArticleComment":
                    _logger.LogInformation("PrivateHistoryService --> EventProcessor: LikedArticleComment event detected!");
                    return EventType.LikedArticleComment;
                case "DislikedArticleComment":
                    _logger.LogInformation("PrivateHistoryService --> EventProcessor: DislikedArticleComment event detected!");
                    return EventType.DislikedArticleComment;
                case "TopicSubscription":
                    _logger.LogInformation("PrivateHistoryService --> EventProcessor: TopicSubscription event detected!");
                    return EventType.TopicSubscription;
                case "SearchedArticleData":
                    _logger.LogInformation("PrivateHistoryService --> EventProcessor: SearchedArticleData event detected!");
                    return EventType.SearchedArticleData;
                default:
                    return EventType.Undetermined;
            }
        }

        private async Task TryToWriteUserAsync(Guid userId)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var userReadService = scope.ServiceProvider.GetRequiredService<IUserReadService>();

            var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

            var userFactory = scope.ServiceProvider.GetRequiredService<IUserFactory>();

            var alreadyExists = await userReadService.ExistsByIdAsync(userId);

            if (alreadyExists is false)
            {
                var user = userFactory.Create(userId);

                await userRepository.AddUserAsync(user);
            }
        }

        private async Task WriteViewedArticleAsync(string article)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var viewedArticleWriteService = scope.ServiceProvider.GetRequiredService<IViewedArticleWriteService>();

            var viewedArticleDto = JsonSerializer.Deserialize<ViewedArticleDto>(article);

            var viewedArticle = _mapper.Map<ViewedArticle>(viewedArticleDto);

            await TryToWriteUserAsync(viewedArticle.UserID);

            await viewedArticleWriteService.AddViewedArticleAsync(viewedArticle);
        }

        private async Task WriteLikedArticleAsync(string likedArticle)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var likedArticleWriteService = scope.ServiceProvider.GetRequiredService<ILikedArticleWriteService>();

            var likedArticleDto = JsonSerializer.Deserialize<LikedArticleDto>(likedArticle);

            var likedArticleValueObject = _mapper.Map<LikedArticle>(likedArticleDto);

            await likedArticleWriteService.AddLikedArticleAsync(likedArticleValueObject);
        }

        private async Task WriteDislikedArticleAsync(string dislikedArticle)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var dislikedArticleWriteService = scope.ServiceProvider.GetRequiredService<IDislikedArticleWriteService>();

            var dislikedArticleDto = JsonSerializer.Deserialize<DislikedArticleDto>(dislikedArticle);

            var dislikedArticleValueObject = _mapper.Map<DislikedArticle>(dislikedArticleDto);

            await dislikedArticleWriteService.AddDislikedArticleAsync(dislikedArticleValueObject);
        }

        private async Task WriteCommentedArticleAsync(string commentedArticle)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var commentedArticleWriteService = scope.ServiceProvider.GetRequiredService<ICommentedArticleWriteService>();

            var commentedArticleDto = JsonSerializer.Deserialize<CommentedArticleDto>(commentedArticle);

            var commentedArticleValueObject = _mapper.Map<CommentedArticle>(commentedArticleDto);

            await commentedArticleWriteService.AddCommentedArticleAsync(commentedArticleValueObject);
        }

        private async Task WriteLikedArticleCommentAsync(string likedArticleComment)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var likedArticleCommentWriteService = scope.ServiceProvider.GetRequiredService<ILikedArticleCommentWriteService>();

            var likedArticleCommentDto = JsonSerializer.Deserialize<LikedArticleCommentDto>(likedArticleComment);

            var likedArticleCommentValueObject = _mapper.Map<LikedArticleComment>(likedArticleCommentDto);

            await likedArticleCommentWriteService.AddLikedArticleCommentAsync(likedArticleCommentValueObject);
        }

        private async Task WriteDislikedArticleCommentAsync(string dislikedArticleComment)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var dislikedArticleCommentWriteService = scope.ServiceProvider.GetRequiredService<IDislikedArticleCommentWriteService>();

            var dislikedArticleCommentDto = JsonSerializer.Deserialize<DislikedArticleCommentDto>(dislikedArticleComment);

            var dislikedArticleCommentValueObject = _mapper.Map<DislikedArticleComment>(dislikedArticleCommentDto);

            await dislikedArticleCommentWriteService.AddDislikedArticleCommentAsync(dislikedArticleCommentValueObject);
        }

        private async Task WriteTopicSubscriptionAsync(string topicSubscription)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var topicSubscriptionWriteService = scope.ServiceProvider.GetRequiredService<ITopicSubscriptionWriteService>();

            var topicSubscriptionDto = JsonSerializer.Deserialize<TopicSubscriptionDto>(topicSubscription);

            var topicSubscriptionValueObject = _mapper.Map<TopicSubscription>(topicSubscriptionDto);

            await TryToWriteUserAsync(topicSubscriptionValueObject.UserID);

            await topicSubscriptionWriteService.AddTopicSubscriptionAsync(topicSubscriptionValueObject);
        }

        private async Task WriteSearchedArticleDataAsync(string searchedArticleData)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var searchedArticleDataWriteService = scope.ServiceProvider.GetRequiredService<ISearchedArticleDataWriteService>();

            var searchedArticleDataDto = JsonSerializer.Deserialize<SearchedArticleDataDto>(searchedArticleData);

            var searchedArticleDataValueObject = _mapper.Map<SearchedArticleData>(searchedArticleDataDto);

            await searchedArticleDataWriteService.AddSearchedArticleDataAsync(searchedArticleDataValueObject);
        }

        public async Task ProcessEventAsync(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.ViewedArticle:
                    await WriteViewedArticleAsync(message);
                    break;
                case EventType.LikedArticle:
                    await WriteLikedArticleAsync(message);
                    break;
                case EventType.DislikedArticle:
                    await WriteDislikedArticleAsync(message);
                    break;
                case EventType.CommentedArticle:
                    await WriteCommentedArticleAsync(message);
                    break;
                case EventType.LikedArticleComment:
                    await WriteLikedArticleCommentAsync(message);
                    break;
                case EventType.DislikedArticleComment:
                    await WriteDislikedArticleCommentAsync(message);
                    break;
                case EventType.TopicSubscription:
                    await WriteTopicSubscriptionAsync(message);
                    break;
                case EventType.SearchedArticleData:
                    await WriteSearchedArticleDataAsync(message);
                    break;
            }
        }
    }
}
