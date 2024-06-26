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
using PrivateHistoryService.Application.Services.ReadServices;
using PrivateHistoryService.Infrastructure.EntityFramework.Services.ReadServices;
using PrivateHistoryService.Infrastructure.Exceptions.Interfaces;
using PrivateHistoryService.Infrastructure.Exceptions;
using PrivateHistoryService.Infrastructure.AsyncDataServices;
using PrivateHistoryService.Infrastructure.EventProcessing;
using PrivateHistoryService.Application.Services.WriteServices;
using PrivateHistoryService.Infrastructure.EntityFramework.Services.WriteServices;
using PrivateHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;
using PrivateHistoryService.Application.Commands.Handlers;
using PrivateHistoryService.Infrastructure.Logging;
using Quartz;
using PrivateHistoryService.Infrastructure.Schedulers;
using Quartz.Simpl;

namespace PrivateHistoryService.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var postgresOptions = configuration.GetOptions<PostgresOptions>("Postgres");

            services.AddSingleton<IEncryptionProvider>(new EncryptionProvider("A1B2C3D4E5F60789"));

            services.AddDbContext<ReadDbContext>(options => options.UseNpgsql(postgresOptions.ConnectionString));

            services.AddDbContext<WriteDbContext>(options => options.UseNpgsql(postgresOptions.ConnectionString));

            services.AddHostedService<DbInitializer>();

            services.AddScoped<IUserRepository, PostgresUserRepository>();

            services.AddScoped<IUserReadService, PostgresUserReadService>();

            services.AddScoped<ICommentedArticleReadService, PostgresCommentedArticleReadService>();

            services.AddScoped<ISearchedArticleDataReadService, PostgresSearchedArticleDataReadService>();

            services.AddScoped<IViewedArticleReadService, PostgresViewedArticleReadService>();

            services.AddScoped<ISearchedTopicDataReadService, PostgresSearchedTopicDataReadService>();

            services.AddScoped<IViewedUserReadService, PostgresViewedUserReadService>();

            services.AddScoped<IViewedArticleWriteService, PostgresViewedArticleWriteService>();

            services.AddScoped<ILikedArticleWriteService, PostgresLikedArticleWriteService>();

            services.AddScoped<IDislikedArticleWriteService, PostgresDislikedArticleWriteService>();

            services.AddScoped<ICommentedArticleWriteService, PostgresCommentedArticleWriteService>();

            services.AddScoped<ILikedArticleCommentWriteService, PostgresLikedArticleCommentWriteService>();

            services.AddScoped<IDislikedArticleCommentWriteService, PostgresDislikedArticleCommentWriteService>();

            services.AddScoped<ITopicSubscriptionWriteService, PostgresTopicSubscriptionWriteService>();

            services.AddScoped<ISearchedArticleDataWriteService, PostgresSearchedArticleDataWriteService>();

            services.AddScoped<IUserWriteService, PostgresUserWriteService>();

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

            services.AddQuartz(configurator =>
            {
                configurator.UseJobFactory<MicrosoftDependencyInjectionJobFactory>();

                var jobKey = new JobKey("RetentionJob");

                configurator.AddJob<RetentionJob>(opts => opts.WithIdentity(jobKey));

                configurator.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("RetentionJob-trigger")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                    //.WithIntervalInMinutes(1)    
                    .WithIntervalInHours(24) // Set the interval to 24 hours.
                    .RepeatForever()));
            });

            services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

            services.AddSingleton<IEventProcessor, EventProcessor>();

            services.AddHostedService<MessageBusSubscriber>();

            services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));

            services.AddSingleton<IExceptionToResponseMapper, ExceptionToResponseMapper>();

            return services;
        }
    }
}
