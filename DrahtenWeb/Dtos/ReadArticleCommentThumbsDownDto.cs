﻿namespace DrahtenWeb.Dtos
{
    public class ReadArticleCommentThumbsDownDto
    {
        public int ArticleCommentId { get; set; }
        public string ArticleId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
    }
}
