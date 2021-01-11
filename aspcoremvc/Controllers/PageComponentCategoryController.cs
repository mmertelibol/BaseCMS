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
    public class PageComponentCategoryController : Controller
    {
        private readonly IPageComponentCategoryService _pageComponentCategoryService;
        private readonly ILogger<PageComponentCategoryController> _logger;
        private readonly IPageComponentService _pageComponentService;

        public PageComponentCategoryController(IPageComponentCategoryService pageComponentCategoryService, ILogger<PageComponentCategoryController> logger, IPageComponentService pageComponentService)
        {
            _pageComponentCategoryService = pageComponentCategoryService;
            _logger = logger;
            _pageComponentService = pageComponentService;
        }
        public IActionResult Index()
        {
            var categoryList = _pageComponentCategoryService.GetAllPageComponentCategories();
            return View(categoryList);
        }

        [HttpPost]
        public JsonResult AddPageComponentCategory(PageComponentCategoryDto pageComponentCategoryDto)
        {
            var added = _pageComponentCategoryService.AddPageComponentCategory(pageComponentCategoryDto);
            return Json(added);
        }

        [HttpDelete]
        public JsonResult DeletePageComponentCategory(int id)
        {
            var pageComponent = _pageComponentService.GetPageComponentByCategoryId(id);
            if (pageComponent.Count == 0)
            {
                var deleted = _pageComponentCategoryService.DeletePageComponentCategory(id);
                return Json(deleted);
            }
            return Json(false);
        }

      

        [HttpPost]
        public JsonResult UpdatePageComponentCategory(PageComponentCategoryDto pageComponentCategoryDto)
        {
            var updated = _pageComponentCategoryService.UpdatePageComponentCategory(pageComponentCategoryDto);
            return Json(updated);
        }

        public JsonResult GetPageComponentCategoryById(int id)
        {
            var pageComponentCategory = _pageComponentCategoryService.GetPageComponentCategoryById(id);
            return Json(pageComponentCategory);
        }
    }
}
