﻿using Microsoft.AspNetCore.Mvc;
using TopicArticleService.Application.Commands;
using TopicArticleService.Application.Commands.Dispatcher;
using TopicArticleService.Application.Dtos;
using TopicArticleService.Application.Queries;
using TopicArticleService.Application.Queries.Dispatcher;

namespace TopicArticleService.Presentation.Controllers
{
    [ApiController]
    [Route("topic-article-service/articles")]
    public class ArticlesController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public ArticlesController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet("{ArticleId:guid}")]
        public async Task<ActionResult<ArticleDto>> GetArticle([FromRoute] GetArticleQuery getArticleQuery)
        {
            var result = await _queryDispatcher.DispatchAsync(getArticleQuery);

            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> RegisterArticle([FromBody] CreateArticleCommand createArticleCommand)
        {
            await _commandDispatcher.DispatchAsync(createArticleCommand);

            return CreatedAtAction(nameof(GetArticle), new { Id = createArticleCommand.ArticleId }, null);
        }

        [HttpPost("{ArticleId:guid}/likes/")]
        public async Task<ActionResult> RegisterArticleLike([FromBody] AddArticleLikeCommand addArticleLikeCommand)
        {
            await _commandDispatcher.DispatchAsync(addArticleLikeCommand);

            return Created(HttpContext.Request.Path, null);
        }

        [HttpPost("{ArticleId:guid}/dislikes/")]
        public async Task<ActionResult> RegisterArticleDislike([FromBody] AddArticleDislikeCommand addArticleDislikeCommand)
        {
            await _commandDispatcher.DispatchAsync(addArticleDislikeCommand);

            return Created(HttpContext.Request.Path, null);
        }

        [HttpPost("{ArticleId:guid}/comments/")]
        public async Task<ActionResult> RegisterArticleComment([FromBody] AddArticleCommentCommand addArticleCommentCommand)
        {
            await _commandDispatcher.DispatchAsync(addArticleCommentCommand);

            return Created(HttpContext.Request.Path, null);
        }
    }
}