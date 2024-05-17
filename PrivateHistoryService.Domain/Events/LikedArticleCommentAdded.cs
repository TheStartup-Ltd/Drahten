﻿using PrivateHistoryService.Domain.Entities;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Domain.Events
{
    public record LikedArticleCommentAdded(User User, LikedArticleComment LikedArticleComment) : IDomainEvent;
}
