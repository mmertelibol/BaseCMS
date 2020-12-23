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
    public class SocialMediaController : Controller
    {
        private readonly ILogger<SocialMediaController> _logger;
        private readonly ISocialMediaService _socialMediaService;

        public SocialMediaController(ILogger<SocialMediaController> logger, ISocialMediaService socialMediaService)
        {
            _logger = logger;
            _socialMediaService = socialMediaService;
        }
        public IActionResult Index()
        {
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

        [HttpPut]
        public JsonResult UpdateSocialMedia(SocialMediaDto socialMediaDto)
        {
            var updated = _socialMediaService.UpdateSocialMedia(socialMediaDto);
            return Json(updated);
        }






    }
}
