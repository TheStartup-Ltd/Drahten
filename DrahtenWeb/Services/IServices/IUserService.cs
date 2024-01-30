﻿using DrahtenWeb.Dtos;

namespace DrahtenWeb.Services.IServices
{
    public interface IUserService : IBaseService
    {
        Task<TEntity> GetUserById<TEntity>(string userId, string accessToken);
        Task<TEntity> RegisterUser<TEntity>(WriteUserDto user, string accessToken);
        Task<TEntity> RegisterUserTopic<TEntity>(WriteUserDto user, string accessToken);
        Task<TEntity> GetTopics<TEntity>(string accessToken);
    }
}
