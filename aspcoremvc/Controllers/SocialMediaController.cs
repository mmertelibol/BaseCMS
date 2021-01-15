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
    public class SocialMediaController : BaseController
    {
        private readonly ILogger<SocialMediaController> _logger;
        private readonly ISocialMediaService _socialMediaService;
        private readonly ISettingService _settingService;

        public SocialMediaController(ILogger<SocialMediaController> logger, ISocialMediaService socialMediaService, ISettingService settingService)
        {
            _logger = logger;
            _socialMediaService = socialMediaService;
            _settingService = settingService;
        }
        public IActionResult Index()
        {
            var favIcon = _settingService.GetSetting().FavIconUrl;
            ViewBag.FavIcon = favIcon;

            var socialMediaList = _socialMediaService.GetAllList();

            return View(socialMediaList);
        }

        [HttpPost]
        public JsonResult AddSocialMedia(SocialMediaDto socialMediaDto)
        {
           
                var added = _socialMediaService.AddSocialMedia(socialMediaDto);
                return Json(added);
            
           
            
        }


        [HttpDelete]
        public JsonResult DeleteSocialMedia(int id)
        {
            var deleted = _socialMediaService.DeleteSocialMedia(id);
            return Json(deleted);
        }

        [HttpPost]
        public JsonResult UpdateSocialMedia(SocialMediaDto socialMediaDto)
        {
            var updated = _socialMediaService.UpdateSocialMedia(socialMediaDto);
            return Json(updated);
        }


        public JsonResult GetSocialMediaById(int id)
        {
            var socialMedia = _socialMediaService.GetSocialMediaById(id);
            return Json(socialMedia);
        }



    }
}
