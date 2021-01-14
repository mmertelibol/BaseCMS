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
            var favIcon = _settingService.GetSetting().FavIconUrl;
            ViewBag.FavIcon = favIcon;
            var setting = _settingService.GetSetting();
            return View(setting);
        }
        
        public JsonResult UpdateSetting(SettingDto settingDto)
        {
            var setting = _settingService.UpdateSetting(settingDto);
            return Json(setting);
        }

      

    }
}
