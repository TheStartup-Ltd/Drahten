﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PrivateHistoryService.Domain.Repositories;
using PrivateHistoryService.Application.Extensions;
using PrivateHistoryService.Infrastructure.EntityFramework.Contexts;
using PrivateHistoryService.Infrastructure.EntityFramework.Initialization;
using PrivateHistoryService.Infrastructure.EntityFramework.Options;
using PrivateHistoryService.Infrastructure.EntityFramework.Repositories;
using PrivateHistoryService.Infrastructure.Automapper.Profiles;

namespace PrivateHistoryService.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var postgresOptions = configuration.GetOptions<PostgresOptions>("Postgres");

            services.AddDbContext<ReadDbContext>(options => options.UseNpgsql(postgresOptions.ConnectionString));

            services.AddDbContext<WriteDbContext>(options => options.UseNpgsql(postgresOptions.ConnectionString));

            services.AddHostedService<DbInitializer>();

            services.AddScoped<IUserRepository, PostgresUserRepository>();

            services.AddQueriesWithDispatcher();

            services.AddAutoMapper(configAction =>
            {
                configAction.AddProfile<CommentedArticleProfile>();
                configAction.AddProfile<DislikedArticleCommentProfile>();
                configAction.AddProfile<DislikedArticleProfile>();
                configAction.AddProfile<LikedArticleCommentProfile>();
                configAction.AddProfile<LikedArticleProfile>();
                configAction.AddProfile<SearchedArticleDataProfile>();
                configAction.AddProfile<SearchedTopicDataProfile>();
                configAction.AddProfile<TopicSubscriptionProfile>();
                configAction.AddProfile<ViewedArticleProfile>();
                configAction.AddProfile<ViewedUserProfile>();
            });

            return services;
        }
    }
}