﻿using DrahtenWeb.Dtos;
using DrahtenWeb.Dtos.PrivateHistoryService;
using DrahtenWeb.Extensions;
using DrahtenWeb.Models;
using DrahtenWeb.Services.IServices;
using DrahtenWeb.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DrahtenWeb.Controllers
{
    [Authorize]
    public class PrivateHistoryController : Controller
    {
        private readonly IPrivateHistoryService _privateHistoryService;

        public PrivateHistoryController(IPrivateHistoryService privateHistoryService)
        {
            _privateHistoryService = privateHistoryService;
        }

        [HttpGet]
        public async Task<IActionResult> ViewedArticles(int pageNumber = 1)
        {
            try
            {
                //Get the user id.
                //Here the NameIdentifier claim type represents the user id.
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var response = await _privateHistoryService.GetViewedArticlesAsync<ResponseDto>(userId, accessToken);

                if (pageNumber < 1)
                {
                    pageNumber = 1;
                }

                const int pageSize = 5;

                var allArticles = response.Map<List<ViewedArticleDto>>();

                var tempList = new List<ViewedArticleDto>
                {
                    new ViewedArticleDto
                    {
                        ViewedArticleId = Guid.NewGuid(),
                        ArticleId = "123",
                        UserId = "1",
                        DateTime = DateTimeOffset.Now
                    },
                    new ViewedArticleDto
                    {
                        ViewedArticleId = Guid.NewGuid(),
                        ArticleId = "123",
                        UserId = "1",
                        DateTime = DateTimeOffset.Now
                    },
                    new ViewedArticleDto
                    {
                        ViewedArticleId = Guid.NewGuid(),
                        ArticleId = "123",
                        UserId = "1",
                        DateTime = DateTimeOffset.Now
                    },
                    new ViewedArticleDto
                    {
                        ViewedArticleId = Guid.NewGuid(),
                        ArticleId = "123",
                        UserId = "1",
                        DateTime = DateTimeOffset.Now
                    },
                    new ViewedArticleDto
                    {
                        ViewedArticleId = Guid.NewGuid(),
                        ArticleId = "123",
                        UserId = "1",
                        DateTime = DateTimeOffset.Now
                    },
                    new ViewedArticleDto
                    {
                        ViewedArticleId = Guid.NewGuid(),
                        ArticleId = "123",
                        UserId = "1",
                        DateTime = DateTimeOffset.Now
                    },
                    new ViewedArticleDto
                    {
                        ViewedArticleId = Guid.NewGuid(),
                        ArticleId = "123",
                        UserId = "1",
                        DateTime = DateTimeOffset.Now
                    },
                    new ViewedArticleDto
                    {
                        ViewedArticleId = Guid.NewGuid(),
                        ArticleId = "123",
                        UserId = "1",
                        DateTime = DateTimeOffset.Now
                    },
                    new ViewedArticleDto
                    {
                        ViewedArticleId = Guid.NewGuid(),
                        ArticleId = "123",
                        UserId = "1",
                        DateTime = DateTimeOffset.Now
                    },
                    new ViewedArticleDto
                    {
                        ViewedArticleId = Guid.NewGuid(),
                        ArticleId = "123",
                        UserId = "1",
                        DateTime = DateTimeOffset.Now
                    },
                    new ViewedArticleDto
                    {
                        ViewedArticleId = Guid.NewGuid(),
                        ArticleId = "123",
                        UserId = "1",
                        DateTime = DateTimeOffset.Now
                    },
                    new ViewedArticleDto
                    {
                        ViewedArticleId = Guid.NewGuid(),
                        ArticleId = "123",
                        UserId = "1",
                        DateTime = DateTimeOffset.Now
                    },
                    new ViewedArticleDto
                    {
                        ViewedArticleId = Guid.NewGuid(),
                        ArticleId = "123",
                        UserId = "1",
                        DateTime = DateTimeOffset.Now
                    },
                    new ViewedArticleDto
                    {
                        ViewedArticleId = Guid.NewGuid(),
                        ArticleId = "123",
                        UserId = "1",
                        DateTime = DateTimeOffset.Now
                    },
                    new ViewedArticleDto
                    {
                        ViewedArticleId = Guid.NewGuid(),
                        ArticleId = "123",
                        UserId = "1",
                        DateTime = DateTimeOffset.Now
                    }
                };

                int articlesCount = tempList.Count;

                var pagination = new Pagination(articlesCount, pageNumber, pageSize);

                int skipArticles = (pageNumber - 1) * pageSize;

                var articles = tempList.Skip(skipArticles).Take(pagination.PageSize).ToList();

                var historyArticleViewModel = new HistoryArticleViewModel
                {
                    Articles = articles,
                    Pagination = pagination
                };


                //int articlesCount = allArticles.Count;

                //var pagination = new Pagination(articlesCount, pageNumber, pageSize);

                //int skipArticles = (pageNumber - 1) * pageSize;

                //var articles = allArticles.Skip(skipArticles).Take(pagination.PageSize).ToList();

                //var historyArticleViewModel = new HistoryArticleViewModel
                //{
                //    Articles = articles,
                //    Pagination = pagination
                //};

                return new JsonResult(historyArticleViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ViewedUsers(int pageNumber = 1)
        {
            try
            {
                //Get the user id.
                //Here the NameIdentifier claim type represents the user id.
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var response = await _privateHistoryService.GetViewedUsersAsync<ResponseDto>(userId, accessToken);

                if (pageNumber < 1)
                {
                    pageNumber = 1;
                }

                const int pageSize = 5;

                var allViewedUsers = response.Map<List<ViewedUserDto>>();

                var tempList = new List<ViewedUserDto>
                {
                   new ViewedUserDto
                   {
                       ViewedUserReadModelId = Guid.NewGuid(),
                       ViewerUserId = "111",
                       ViewedUserId = "122",
                       DateTime = DateTimeOffset.Now
                   },
                   new ViewedUserDto
                   {
                       ViewedUserReadModelId = Guid.NewGuid(),
                       ViewerUserId = "121",
                       ViewedUserId = "123",
                       DateTime = DateTimeOffset.Now
                   }
                };

                int viewedUsersCount = tempList.Count;

                var pagination = new Pagination(viewedUsersCount, pageNumber, pageSize);

                int skipUsers = (pageNumber - 1) * pageSize;

                var viewedUsers = tempList.Skip(skipUsers).Take(pagination.PageSize).ToList();

                var historyUserViewModel = new HistoryUserViewModel
                {
                    ViewedUsers = viewedUsers,
                    Pagination = pagination,
                };

                return new JsonResult(historyUserViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
