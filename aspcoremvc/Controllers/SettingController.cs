using Business.Services.Panel.Interfaces;
using Common.Dto.PanelDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class SettingController : Controller
    {
        private readonly ISettingService _settingService;
        private readonly ILogger<SettingController> _logger;

        public SettingController(ISettingService settingService, ILogger<SettingController> logger)
        {
            _logger = logger;
            _settingService = settingService;
        }

        public IActionResult Index()
        {
            var settingList = _settingService.GetAllSettings();
            return View(settingList);
        }
        public JsonResult AddSetting(SettingDto settingdto)
        {
            var added = _settingService.AddSetting(settingdto);



            return Json(added);
        }
        public JsonResult DeleteSetting()
        {
            return Json("");
        }
        public JsonResult UpdateSetting()
        {
            return Json("");
        }

        public JsonResult GetSettingById()
        {
            return Json("");
        }

    }
}
