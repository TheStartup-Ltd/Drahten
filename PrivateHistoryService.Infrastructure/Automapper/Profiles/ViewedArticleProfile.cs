﻿using AutoMapper;
using PrivateHistoryService.Application.Dtos;
using PrivateHistoryService.Application.Extensions;
using PrivateHistoryService.Domain.ValueObjects;
using PrivateHistoryService.Infrastructure.EntityFramework.Models;

namespace PrivateHistoryService.Infrastructure.Automapper.Profiles
{
    internal sealed class ViewedArticleProfile : Profile
    {
        public ViewedArticleProfile()
        {
            CreateMap<ViewedArticleReadModel, ViewedArticleDto>()
                .ForMember(dest => dest.RetentionUntil, options => options.MapFrom(source => source.User.RetentionUntil));

            CreateMap<ViewedArticleDto, ViewedArticle>()
                .ConstructUsing(source => 
                    new ViewedArticle(Guid.Parse(source.ArticleId), Guid.Parse(source.UserId), source.DateTime.ToUtc()));
        }
    }
}
