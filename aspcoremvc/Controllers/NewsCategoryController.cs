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
    public class NewsCategoryController : Controller
    {
        private readonly INewsCategoryService _newsCategoryService;
        private readonly INewsService _newsService;
        private readonly ILogger<NewsCategoryController> _logger;
        

        public NewsCategoryController(INewsCategoryService newsCategoryService, ILogger<NewsCategoryController> logger, INewsService newsService)
        {
            _newsCategoryService = newsCategoryService;
            _logger = logger;
            _newsService = newsService;
        }
        public IActionResult Index()
        {
            var categorylist = _newsCategoryService.GetAllNewsCategories();
            //var categories = categorylist.GroupBy(u=>new { u.Name}).Select(grp=>grp.FirstOrDefault()).ToList();
            //ViewBag.Categories = categories;
            return View(categorylist);
        }

        [HttpPost]
        public JsonResult AddNewsCategory(NewsCategoryDto newsCategoryDto)
        {
            var added = _newsCategoryService.AddNewsCategory(newsCategoryDto);
            return Json(added);
        }

        [HttpDelete]
        public JsonResult DeleteNewsCategory(int id)
        {
            var newsList = _newsService.GetNewsByCategoryId(id);
            if (newsList.Count==0)
            {
                var deleted = _newsCategoryService.DeleteNewsCategory(id);
                return Json(deleted);
            }
            return Json(false);
        }

           
            
            
        

        [HttpPost]
        public JsonResult UpdateNewsCategory(NewsCategoryDto newsCategoryDto)
        {
            var updated = _newsCategoryService.UpdateNewsCategory(newsCategoryDto);
            return Json(updated);
        }

        public JsonResult GetNewsCategoryById(int id)
        {
            var newsCategory = _newsCategoryService.GetNewsCategoryById(id);
            return Json(newsCategory);
        }
    }
}
