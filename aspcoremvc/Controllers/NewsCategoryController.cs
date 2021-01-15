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
    public class NewsCategoryController : BaseController
    {
        private readonly INewsCategoryService _newsCategoryService;
        private readonly INewsService _newsService;
        private readonly ILogger<NewsCategoryController> _logger;
        private readonly ISettingService _settingService;
        

        public NewsCategoryController(INewsCategoryService newsCategoryService, ILogger<NewsCategoryController> logger, INewsService newsService, ISettingService settingService)
        {
            _newsCategoryService = newsCategoryService;
            _logger = logger;
            _newsService = newsService;
            _settingService = settingService;
        }
        public IActionResult Index()
            
        {
            var favIcon = _settingService.GetSetting().FavIconUrl;
            ViewBag.FavIcon = favIcon;
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
        public IActionResult DeleteNewsCategory(int id)
        {
            
            var newsList = _newsService.GetNewsByNewsCategoryId(id);

            if (newsList.Count==0)
            {
                 var deletedNews= _newsCategoryService.DeleteNewsCategory(id);
                return Json(deletedNews);      
            }

            return Json(false);

        }


        [HttpPost]
        public  JsonResult DeleteAndAssignNewsCategory(NewsCategoryDto newsCategoryDto)
        {
            
            var deletedAndAssignedCategory = _newsCategoryService.DeleteAndAssignNewsCategory(newsCategoryDto);

            if (newsCategoryDto.Id != newsCategoryDto.NewsCategoryId)
            {
                return Json(deletedAndAssignedCategory);
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
