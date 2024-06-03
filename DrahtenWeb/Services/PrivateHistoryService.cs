﻿using DrahtenWeb.Models;
using DrahtenWeb.Services.IServices;

namespace DrahtenWeb.Services
{
    public class PrivateHistoryService : BaseService, IPrivateHistoryService
    {
        public PrivateHistoryService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        { 
        }

        public async Task<TEntity> GetCommentedArticlesAsync<TEntity>(Guid userId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"https://localhost:7076/privatehistory-service/users/{userId}/commented-articles/",
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> GetLikedArticlesAsync<TEntity>(Guid userId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"https://localhost:7076/privatehistory-service/users/{userId}/liked-articles/",
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> GetDislikedArticlesAsync<TEntity>(Guid userId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"https://localhost:7076/privatehistory-service/users/{userId}/disliked-articles/",
                AccessToken = accessToken
            });

            return response;
        }

        public Task<TEntity> GetLikedArticleCommentsAsync<TEntity>(Guid userId, string accessToken)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetDislikedArticleCommentsAsync<TEntity>(Guid userId, string accessToken)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetSearchedArticlesAsync<TEntity>(Guid userId, string accessToken)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetSearchedTopicsAsync<TEntity>(Guid userId, string accessToken)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetTopicSubscriptionsAsync<TEntity>(Guid userId, string accessToken)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetViewedArticlesAsync<TEntity>(Guid userId, string accessToken)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetViewedUsersAsync<TEntity>(Guid userId, string accessToken)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> RemoveCommentedArticleAsync<TEntity>(Guid userId, Guid commentedArticleId, string accessToken)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> RemoveSearchedArticleDataAsync<TEntity>(Guid userId, Guid searchedArticleDataId, string accessToken)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> RemoveSearchedTopicDataAsync<TEntity>(Guid userId, Guid searchedTopicDataId, string accessToken)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> RemoveTopicSubscriptionAsync<TEntity>(Guid userId, Guid topicId, string accessToken)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> RemoveViewedArticleAsync<TEntity>(Guid userId, Guid viewedArticleId, string accessToken)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> RemoveViewedUserAsync<TEntity>(Guid userId, Guid viewedUserId, string accessToken)
        {
            throw new NotImplementedException();
        }
    }
}