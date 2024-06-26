﻿using PrivateHistoryService.Domain.Events;
using PrivateHistoryService.Domain.Exceptions;
using PrivateHistoryService.Domain.ValueObjects;
using System.Collections.ObjectModel;

namespace PrivateHistoryService.Domain.Entities
{
    public class User : AggregateRoot<UserID>
    {
        private UserRetentionUntil _userRetention;
        private HashSet<ViewedArticle> _viewedArticles = new HashSet<ViewedArticle>();
        private List<TopicSubscription> _subscribedTopics = new List<TopicSubscription>();
        private HashSet<SearchedArticleData> _searchedArticleInformation = new HashSet<SearchedArticleData>();
        private HashSet<SearchedTopicData> _searchedTopicInformation = new HashSet<SearchedTopicData>();
        private HashSet<CommentedArticle> _commentedArticles = new HashSet<CommentedArticle>();
        private List<LikedArticle> _likedArticles = new List<LikedArticle>();
        private List<DislikedArticle> _dislikedArticles = new List<DislikedArticle>();
        private List<LikedArticleComment> _likedArticleComments = new List<LikedArticleComment>();
        private List<DislikedArticleComment> _dislikedArticleComments = new List<DislikedArticleComment>();
        private HashSet<ViewedUser> _viewedUsers = new HashSet<ViewedUser>();

        public IReadOnlyCollection<ViewedArticle> ViewedArticles
        {
            get { return new ReadOnlyCollection<ViewedArticle>(_viewedArticles.ToList()); }
        }

        public IReadOnlyCollection<TopicSubscription> SubscribedTopics
        {
            get { return new ReadOnlyCollection<TopicSubscription>(_subscribedTopics); }
        }

        public IReadOnlyCollection<SearchedArticleData> SearchedArticleInformation
        {
            get { return new ReadOnlyCollection<SearchedArticleData>(_searchedArticleInformation.ToList()); }
        }

        public IReadOnlyCollection<SearchedTopicData> SearchedTopicInformation
        {
            get { return new ReadOnlyCollection<SearchedTopicData>(_searchedTopicInformation.ToList()); }
        }

        public IReadOnlyCollection<CommentedArticle> CommentedArticles
        {
            get { return new ReadOnlyCollection<CommentedArticle>(_commentedArticles.ToList()); }
        }

        public IReadOnlyCollection<LikedArticle> LikedArticles
        {
            get { return new ReadOnlyCollection<LikedArticle>(_likedArticles); }
        }

        public IReadOnlyCollection<DislikedArticle> DislikedArticles
        {
            get { return new ReadOnlyCollection<DislikedArticle>(_dislikedArticles); }
        }

        public IReadOnlyCollection<LikedArticleComment> LikedArticleComments
        {
            get { return new ReadOnlyCollection<LikedArticleComment>(_likedArticleComments); }
        }

        public IReadOnlyCollection<DislikedArticleComment> DislikedArticleComments
        {
            get { return new ReadOnlyCollection<DislikedArticleComment>(_dislikedArticleComments); }
        }

        public IReadOnlyCollection<ViewedUser> ViewedUsers
        {
            get { return new ReadOnlyCollection<ViewedUser>(_viewedUsers.ToList()); }
        }

        private User()
        {
        }

        internal User(UserID userId)
        {
            ValidateConstructorParameters<NullUserParametersException>([userId]);

            Id = userId;
        }

        public bool HasUserRetentionDateTime()
            => _userRetention != null;

        public bool IsUserRetentionDateTimeExpired(DateTimeOffset dateTime)
            => _userRetention.RetentionUntil < dateTime;

        public void SetUserRetentionDateTime(UserRetentionUntil userRetentionUntil)
        {
            if(userRetentionUntil == null)
            {
                throw new NullUserRetentionUntilException(Id);
            }

            _userRetention = userRetentionUntil;

            AddEvent(new UserRetentionUntilAdded(this, userRetentionUntil));
        }

        public void AddViewedArticle(ViewedArticle viewedArticle)
        {
            var alreadyExists = _viewedArticles.Contains(viewedArticle);

            if (alreadyExists)
            {
                throw new ViewedArticleAlreadyExistsException(viewedArticle.ArticleID, viewedArticle.UserID, viewedArticle.DateTime);
            }

            _viewedArticles.Add(viewedArticle);

            AddEvent(new ViewedArticleAdded(this, viewedArticle));
        }

        public void RemoveViewedArticle(ViewedArticle viewedArticle)
        {
            var alreadyExists = _viewedArticles.Contains(viewedArticle);

            if (alreadyExists == false)
            {
                throw new ViewedArticleNotFoundException(viewedArticle.ArticleID, viewedArticle.UserID, viewedArticle.DateTime);
            }

            _viewedArticles.Remove(viewedArticle);

            AddEvent(new ViewedArticleRemoved(this, viewedArticle));
        }

        public void AddTopicSubscription(TopicSubscription topicSubscription)
        {
            var alreadyExists = _subscribedTopics.Any(x => x.TopicID == topicSubscription.TopicID && x.UserID == topicSubscription.UserID);

            if (alreadyExists)
            {
                throw new SubscribedTopicAlreadyExistsException(topicSubscription.TopicID, topicSubscription.UserID);
            }

            _subscribedTopics.Add(topicSubscription);

            AddEvent(new TopicSubscriptionAdded(this, topicSubscription));
        }

        public void RemoveTopicSubscription(TopicID topicId, UserID userId)
        {
            var topicSubscription = _subscribedTopics.SingleOrDefault(x => x.TopicID == topicId && x.UserID == userId);

            if (topicSubscription == null)
            {
                throw new TopicSubscriptionNotFoundException(topicId, userId);
            }

            _subscribedTopics.Remove(topicSubscription);

            AddEvent(new TopicSubscriptionRemoved(this, topicSubscription));
        }

        public void AddSearchedArticleData(SearchedArticleData searchedArticleData)
        {
            var alreadyExists = _searchedArticleInformation.Contains(searchedArticleData);

            if (alreadyExists) 
            {
                throw new SearchedArticleDataAlreadyExistsException(searchedArticleData.UserID, searchedArticleData.DateTime);
            }

            _searchedArticleInformation.Add(searchedArticleData);

            AddEvent(new SearchedArticleDataAdded(this, searchedArticleData));
        }

        public void RemoveSearchedArticleData(SearchedArticleData searchedArticleData)
        {
            var alreadyExists = _searchedArticleInformation.Contains(searchedArticleData);

            if (alreadyExists == false)
            {
                throw new SearchedArticleDataNotFoundException(searchedArticleData.ArticleID, searchedArticleData.UserID, searchedArticleData.DateTime);
            }

            _searchedArticleInformation.Remove(searchedArticleData);

            AddEvent(new SearchedArticleDataRemoved(this, searchedArticleData));
        }

        public void AddSearchedTopicData(SearchedTopicData searchedTopicData)
        {
            var alreadyExists = _searchedTopicInformation.Contains(searchedTopicData);

            if (alreadyExists)
            {
                throw new SearchedTopicDataAlreadyExistsException(searchedTopicData.UserID, searchedTopicData.DateTime);
            }

            _searchedTopicInformation.Add(searchedTopicData);

            AddEvent(new SearchedTopicDataAdded(this, searchedTopicData));
        }

        public void RemoveSearchedTopicData(SearchedTopicData searchedTopicData)
        {
            var alreadyExists = _searchedTopicInformation.Contains(searchedTopicData);

            if (alreadyExists == false)
            {
                throw new SearchedTopicDataNotFoundException(searchedTopicData.TopicID, searchedTopicData.UserID, searchedTopicData.DateTime);
            }

            _searchedTopicInformation.Remove(searchedTopicData);

            AddEvent(new SearchedTopicDataRemoved(this, searchedTopicData));
        }

        public void AddCommentedArticle(CommentedArticle commentedArticle)
        {
            var alreadyExists = _commentedArticles.Contains(commentedArticle);

            if (alreadyExists)
            {
                throw new CommentedArticleAlreadyExistsException(commentedArticle.UserID, commentedArticle.DateTime);
            }

            _commentedArticles.Add(commentedArticle);

            AddEvent(new CommentedArticleAdded(this, commentedArticle));
        }

        public void RemoveCommentedArticle(CommentedArticle commentedArticle)
        {
            var alreadyExists = _commentedArticles.Contains(commentedArticle);

            if (alreadyExists == false)
            {
                throw new CommentedArticleNotFoundException(commentedArticle.ArticleID, commentedArticle.UserID, 
                    commentedArticle.ArticleComment, commentedArticle.DateTime);
            }

            _commentedArticles.Remove(commentedArticle);

            AddEvent(new CommentedArticleRemoved(this, commentedArticle));
        }

        public void AddLikedArticle(LikedArticle likedArticle)
        {
            var alreadyExists = _likedArticles.Any(x => x.ArticleID == likedArticle.ArticleID && x.UserID == likedArticle.UserID);

            if (alreadyExists)
            {
                throw new LikedArticleAlreadyExistsException(likedArticle.ArticleID, likedArticle.UserID);
            }

            _likedArticles.Add(likedArticle);

            AddEvent(new LikedArticleAdded(this, likedArticle));
        }

        public void AddDislikedArticle(DislikedArticle dislikedArticle)
        {
            var alreadyExists = _dislikedArticles.Any(x => x.ArticleID == dislikedArticle.ArticleID && x.UserID == dislikedArticle.UserID);

            if (alreadyExists)
            {
                throw new DislikedArticleAlreadyExistsException(dislikedArticle.ArticleID, dislikedArticle.UserID);
            }

            _dislikedArticles.Add(dislikedArticle);

            AddEvent(new DislikedArticleAdded(this, dislikedArticle));
        }

        public void AddLikedArticleComment(LikedArticleComment likedArticleComment)
        {
            var alreadyExists = _likedArticleComments.Any(x => x.ArticleID == likedArticleComment.ArticleID 
            && x.UserID == likedArticleComment.UserID && x.ArticleCommentID == likedArticleComment.ArticleCommentID);

            if (alreadyExists)
            {
                throw new LikedArticleCommentAlreadyExistsException(likedArticleComment.ArticleCommentID, likedArticleComment.UserID);
            }

            _likedArticleComments.Add(likedArticleComment);

            AddEvent(new LikedArticleCommentAdded(this, likedArticleComment));
        }

        public void AddDislikedArticleComment(DislikedArticleComment dislikedArticleComment)
        {
            var alreadyExists = _dislikedArticleComments.Any(x => x.ArticleID == dislikedArticleComment.ArticleID
            && x.UserID == dislikedArticleComment.UserID && x.ArticleCommentID == dislikedArticleComment.ArticleCommentID);

            if (alreadyExists)
            {
                throw new DislikedArticleCommentAlreadyExistsException(dislikedArticleComment.ArticleCommentID, dislikedArticleComment.UserID);
            }

            _dislikedArticleComments.Add(dislikedArticleComment);

            AddEvent(new DislikedArticleCommentAdded(this, dislikedArticleComment));
        }

        public void AddViewedUser(ViewedUser viewedUser)
        {
            var alreadyExists = _viewedUsers.Contains(viewedUser);

            if (alreadyExists)
            {
                throw new ViewedUserAlreadyExistsException(viewedUser.ViewedUserId, viewedUser.ViewerUserId);
            }

            _viewedUsers.Add(viewedUser);

            AddEvent(new ViewedUserAdded(this, viewedUser));
        }

        public void RemoveViewedUser(ViewedUser viewedUser)
        {
            var alreadyExists = _viewedUsers.Contains(viewedUser);

            if (alreadyExists == false)
            {
                throw new ViewedUserNotFoundException(viewedUser.ViewedUserId, viewedUser.ViewerUserId, viewedUser.DateTime);
            }

            _viewedUsers.Remove(viewedUser);

            AddEvent(new ViewedUserRemoved(this, viewedUser));
        }
    }
}