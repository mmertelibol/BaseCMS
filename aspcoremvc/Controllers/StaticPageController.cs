﻿using Business.Services.Panel.Interfaces;
using Common.Dto.PanelDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class StaticPageController : Controller
    {
        private readonly IStaticPageService _staticPageService;
        private readonly ILogger<StaticPageController> _logger;

        public StaticPageController(IStaticPageService staticPageService, ILogger<StaticPageController> logger)
        {
            _logger = logger;
            _staticPageService = staticPageService;
        }
        public IActionResult Index()
        {
            var pageList = _staticPageService.GetAllStaticPage();
            return View(pageList);
        }

        public JsonResult AddStaticPage(StaticPageDto staticPageDto)
        {
            var added = _staticPageService.AddStaticPage(staticPageDto);
            return Json(added);
        }

        public JsonResult DeleteStaticPage(int id)
        {
            var deleted = _staticPageService.DeleteStaticPage(id);

            return Json(deleted);
        }
    }
}
