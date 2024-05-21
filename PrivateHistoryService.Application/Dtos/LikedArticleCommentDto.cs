﻿
namespace PrivateHistoryService.Application.Dtos
{
    public class LikedArticleCommentDto
    {
        public Guid ArticleId { get; set; }
        public Guid UserId { get; set; }
        public Guid ArticleCommentId { get; set; }
        public DateTimeOffset DateTime { get; set; }
    }
}
