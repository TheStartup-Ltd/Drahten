﻿using TopicArticleService.Application.AsyncDataServices;
using TopicArticleService.Application.Dtos.PrivateHistoryService;
using TopicArticleService.Application.Exceptions;
using TopicArticleService.Application.Extensions;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.Repositories;

namespace TopicArticleService.Application.Commands.Handlers
{
    internal sealed class AddArticleCommentHandler : ICommandHandler<AddArticleCommentCommand>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleCommentFactory _articleCommentFactory;
        private readonly IMessageBusPublisher _messageBusPublisher;

        public AddArticleCommentHandler(IArticleRepository articleRepository, IArticleCommentFactory articleCommentFactory,
            IMessageBusPublisher messageBusPublisher)
        {
            _articleRepository = articleRepository;
            _articleCommentFactory = articleCommentFactory;
            _messageBusPublisher = messageBusPublisher;
        }

        public async Task HandleAsync(AddArticleCommentCommand command)
        {
            var article = await _articleRepository.GetArticleByIdAsync(command.ArticleId);

            if (article == null)
            {
                throw new ArticleNotFoundException(command.ArticleId);
            }

            var commentedArticleDto = new CommentedArticleDto
            {
                ArticleId = command.ArticleId.ToString(),
                UserId = command.UserId.ToString(),
                ArticleComment = command.CommentValue,
                DateTime = command.DateTime,
                Event = "CommentedArticle"
            };

            //Post message to the message broker about adding comment for article with ID: ArticleId by user with ID: UserId.
            _messageBusPublisher.PublishCommentedArticle(commentedArticleDto);

            var articleComment = _articleCommentFactory.Create(command.ArticleCommentId, command.CommentValue, 
                command.DateTime.ToUtc(), command.UserId, command.ParentArticleCommentId);

            article.AddComment(articleComment);

            await _articleRepository.UpdateArticleAsync(article);
        }
    }
}
