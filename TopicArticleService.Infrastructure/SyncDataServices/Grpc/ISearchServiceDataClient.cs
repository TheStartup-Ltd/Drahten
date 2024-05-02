﻿
namespace TopicArticleService.Infrastructure.SyncDataServices.Grpc
{
    internal interface ISearchServiceDataClient
    {
        IAsyncEnumerable<(string, Document)> GetDocumentsAsync();
    }
}