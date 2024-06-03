﻿using DrahtenWeb.Services.IServices;

namespace DrahtenWeb.Services
{
    public class PrivateHistoryService : BaseService, IPrivateHistoryService
    {
        public PrivateHistoryService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        { 
        }

        public Task<TEntity> GetCommentedArticlesAsync<TEntity>(Guid userId, string accessToken)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetLikedArticlesAsync<TEntity>(Guid userId, string accessToken)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetDislikedArticlesAsync<TEntity>(Guid userId, string accessToken)
        {
            throw new NotImplementedException();
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
