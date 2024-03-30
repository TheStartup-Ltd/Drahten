﻿using TopicArticleService.Domain.Events;
using TopicArticleService.Domain.Exceptions;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Entities
{
    public class Topic : AggregateRoot<TopicId>
    {
        private TopicName _topicName;
        private TopicId _parentTopicId;
        private List<Topic> _topicChildren = new List<Topic>();

        private Topic()
        {   
        }

        internal Topic(TopicId id, TopicName topicName, TopicId parentTopicId = null)
        {
            Id = id;
            _topicName = topicName;
            _parentTopicId = parentTopicId;
        }

        public void AddTopicChild(Topic topic)
        {
            var alreadyExists = _topicChildren.Any(x => x._topicName == topic._topicName);

            if (alreadyExists)
            {
                throw new TopicChildAlreadyExistsException(Id, topic._topicName);
            }

            _topicChildren.Add(topic);

            AddEvent(new TopicChildAdded(this, topic));
        }
    }
}