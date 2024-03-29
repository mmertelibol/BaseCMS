﻿using Business.Services.Panel.Interfaces;
using Common.Dto.PanelDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class NewsController : BaseController
    {
        private readonly INewsService _newsService;
        private readonly ILogger<NewsController> _logger;
        private readonly INewsCategoryService _newsCategoryService;
        private readonly ISettingService _settingService;
        public NewsController(INewsService newsService, ILogger<NewsController> logger, INewsCategoryService newsCategoryService, ISettingService settingService)
        {
            _newsService = newsService;
            _logger = logger;
            _newsCategoryService = newsCategoryService;
            _settingService = settingService;
        }

        public IActionResult Index()
        {
            var favIcon = _settingService.GetSetting().FavIconUrl;
            ViewBag.FavIcon = favIcon;

            var newsList = _newsService.GetAllNews();
            var categoryList = _newsCategoryService.GetAllNewsCategories().Distinct().ToList();

            ViewBag.Categories = categoryList;


            return View(newsList);
        }

        [HttpPost]

        public JsonResult AddNews(NewsDto newsDto)
        {
            var addednews = _newsService.AddNews(newsDto);
            return Json(newsDto);

        }

        [HttpDelete]
        public JsonResult DeleteNews(int id)
        {
            var deletedNews = _newsService.DeleteNews(id);

            return Json(deletedNews);
        }

        [HttpPost]
        public JsonResult UpdateNews(NewsDto newsDto)
        {
            var updatedNews = _newsService.UpdateNews(newsDto);
            return Json(updatedNews);
        }

        [HttpGet]
        public JsonResult GetNewsById(int id)
        {
            var news = _newsService.GetNewsById(id);
            return Json(news);
        }

    }

}
