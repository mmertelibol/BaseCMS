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
    public class PageComponentController : Controller
    {
        private readonly IPageComponentService _pageComponentService;
        private readonly IPageComponentCategoryService _pageComponentCategoryService;
        private readonly ILogger<PageComponentController> _logger;

        public PageComponentController(IPageComponentService pageComponentService, ILogger<PageComponentController> logger, IPageComponentCategoryService pageComponentCategoryService)
        {
            _logger = logger;
            _pageComponentService = pageComponentService;
            _pageComponentCategoryService = pageComponentCategoryService;
        }
        public IActionResult Index()
        {
            var categoryList = _pageComponentCategoryService.GetAllPageComponentCategories();
            ViewBag.ComponentCategories = categoryList;
           var componentList= _pageComponentService.GetAllPageComponents();
            return View(componentList);
        }

        [HttpPost]
        public JsonResult AddPageComponent(PageComponentDto pageComponentDto)
        {
            var added = _pageComponentService.AddPageComponent(pageComponentDto);
            return Json(added);
        }




    }
}
