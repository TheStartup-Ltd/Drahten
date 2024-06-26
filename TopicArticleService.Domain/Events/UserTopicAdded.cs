﻿using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Events
{
    public record UserTopicAdded(User User, UserTopic UserTopic) : IDomainEvent;
}
