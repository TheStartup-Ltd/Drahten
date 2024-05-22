﻿using AutoMapper;
using PrivateHistoryService.Application.Dtos;
using PrivateHistoryService.Infrastructure.EntityFramework.Models;

namespace PrivateHistoryService.Infrastructure.Automapper.Profiles
{
    internal sealed class SearchedTopicDataProfile : Profile
    {
        public SearchedTopicDataProfile()
        {
            CreateMap<SearchedTopicDataReadModel, SearchedTopicDataDto>();
        }
    }
}
