﻿
namespace TopicArticleService.Application.Dtos
{
    public class ArticleCommentLikeDto
    {
        public Guid ArticleCommentId { get; set; }
        public string UserId { get; set; }
        public DateTimeOffset DateTime { get; set; }
    }
}
