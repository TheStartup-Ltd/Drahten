﻿using AutoMapper;
using PrivateHistoryService.Application.Dtos;
using PrivateHistoryService.Infrastructure.EntityFramework.Models;

namespace PrivateHistoryService.Infrastructure.Automapper.Profiles
{
    internal sealed class LikedArticleCommentProfile : Profile
    {
        internal LikedArticleCommentProfile()
        {
            CreateMap<LikedArticleCommentReadModel, LikedArticleCommentDto>();
        }
    }
}