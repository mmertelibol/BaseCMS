using Business.Services.Panel.Interfaces;
using Common.Dto.PanelDto;
using Common.Dto.PanelDtos;
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
            //var deletedCategory = _newsCategoryService.GetDeletedNewsCategoryName(newsCategoryDto);
            var categorylist = _newsCategoryService.GetAllNewsCategories();
            ViewBag.Categories = categorylist;

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
                 var deletedNews= _newsCategoryService.DeleteNewsCategory(id);
                return Json(deletedNews);      
            }

            return Json(false);

        }


        [HttpPost]
        public JsonResult DeleteAndAssignNewsCategory(NewsCategoryDto newsCategoryDto)
        {
            
            var deletedAndAssignedCategory = _newsCategoryService.DeleteAndAssignNewsCategory(newsCategoryDto);

            return Json(deletedAndAssignedCategory);
            
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
