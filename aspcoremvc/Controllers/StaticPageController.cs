using Business.Services.Panel.Interfaces;
using Common.Dto.PanelDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
        private readonly ISettingService _settingService;
        public StaticPageController(IStaticPageService staticPageService, ILogger<StaticPageController> logger, ISettingService settingService)
        {
            _logger = logger;
            _staticPageService = staticPageService;
            _settingService = settingService;
        }
        public IActionResult Index()
        {
            var favIcon = _settingService.GetSetting().FavIconUrl;
            ViewBag.FavIcon = favIcon;

            var pageList = _staticPageService.GetAllStaticPage();
            return View(pageList);
        }
        [HttpPost]
        public JsonResult AddStaticPage(StaticPageDto staticPageDto)
        {
            var added = _staticPageService.AddStaticPage(staticPageDto);
            return Json(added);
        }

        [HttpDelete]
        public JsonResult DeleteStaticPage(int id)
        {
            var deleted = _staticPageService.DeleteStaticPage(id);

            return Json(deleted);
        }

        [HttpPost]
        public JsonResult UpdateStaticPage(StaticPageDto staticPageDto)
        {
            var updated = _staticPageService.UpdateStaticPage(staticPageDto);

            return Json(JsonConvert.SerializeObject(updated));
        }

        public JsonResult GetStaticPageById(int id)
        {
            var page = _staticPageService.GetStaticPageById(id);
            return Json(JsonConvert.SerializeObject(page));
        }
    }
}
