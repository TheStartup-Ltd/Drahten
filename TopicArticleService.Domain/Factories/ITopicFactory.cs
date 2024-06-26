﻿using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Factories
{
    public interface ITopicFactory
    {
        Topic Create(TopicId topicId, TopicName topicName, TopicFullName topicFullName, TopicId parentTopicId = null);
    }
}
