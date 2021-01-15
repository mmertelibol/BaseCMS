using Business.Services.Panel.Interfaces;
using Common.Dto.PanelDto;
using Common.Dto.PanelDtos;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    public class HomePageController : BaseController
    {
        private readonly IGenericService _genericService;
        private readonly ISettingService _settingService;

        public HomePageController(IGenericService genericService,ISettingService settingService)
        {
            _genericService = genericService;
            _settingService = settingService;
        }
        
        public IActionResult Index()
        {
            var favIcon = _settingService.GetSetting().FavIconUrl;
            ViewBag.FavIcon = favIcon;

            GenericDto dto = new GenericDto();
            return View(_genericService.GetAllDto(dto));
        }
    }
}
