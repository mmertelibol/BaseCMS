using Business.Services.Panel.Interfaces;
using Common.Dto.PanelDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly ILogger<NewsController> _logger;
        public NewsController(INewsService newsService, ILogger<NewsController> logger)
        {
            _newsService = newsService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            var newsList = _newsService.GetAllNews();            

            return View(newsList);
        }

        [HttpPost]
        public JsonResult AddNews(NewsDto newsDto)
        {
            var addednews = _newsService.AddNews(newsDto);
            
            return Json(addednews);
        }

        [HttpDelete]
        public JsonResult DeleteNews(int id)
        {
            var deletedNews = _newsService.DeleteNews(id);

            return Json(deletedNews);

        }

        [HttpPut]
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
