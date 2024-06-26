﻿using PrivateHistoryService.Domain.Exceptions;

namespace PrivateHistoryService.Domain.ValueObjects
{
    public record TopicSubscription
    {
        public TopicID TopicID { get; }
        public UserID UserID { get; }
        internal DateTimeOffset DateTime { get; }

        private TopicSubscription() {}

        public TopicSubscription(TopicID topicId, UserID userId, DateTimeOffset dateTime)
        {
            if (topicId == null)
            {
                throw new NullTopicIdException();
            }

            if (userId == null)
            {
                throw new NullUserIdException();
            }

            if (dateTime == default || dateTime > DateTimeOffset.Now)
            {
                throw new InvalidSubscribedTopicDateTimeException();
            }

            TopicID = topicId;
            UserID = userId;
            DateTime = dateTime;
        }
    }
}
