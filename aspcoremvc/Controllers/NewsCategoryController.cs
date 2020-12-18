using Business.Services.Panel.Interfaces;
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
        private readonly ILogger<NewsCategoryController> _logger;
        

        public NewsCategoryController(INewsCategoryService newsCategoryService, ILogger<NewsCategoryController> logger)
        {
            _newsCategoryService = newsCategoryService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            var categorylist = _newsCategoryService.GetAllNewsCategories();
            //var categories = categorylist.GroupBy(u=>new { u.Name}).Select(grp=>grp.FirstOrDefault()).ToList();
            //ViewBag.Categories = categories;
            return View(categorylist);
        }

        public JsonResult InsertNewsCategory()
        {
            return Json("true");
        }

        public JsonResult DeleteNewsCategory()
        {
            return Json("true");
        }

        public JsonResult UpdateNewsCategory()
        {
            return Json("true");
        }

        public JsonResult GetCategoryById()
        {
            return Json("true");
        }
    }
}
