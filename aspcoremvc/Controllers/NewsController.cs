using Business.Services.Panel.Interfaces;
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
        public JsonResult InsertNews()
        {

            return Json("true");
        }

        [HttpDelete]
        public JsonResult DeleteNews()
        {

            return Json("true");

        }

        [HttpPut]
        public JsonResult UpdateNews()
        {

            return Json("true");

        }

        [HttpGet]
        public JsonResult GetNewsById(int id)
        {

            return Json("true");

        }




    }



}
