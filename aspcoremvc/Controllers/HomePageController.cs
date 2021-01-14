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
    public class HomePageController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISettingService _settingService;

        public HomePageController(AppDbContext context,ISettingService settingService)
        {
            _context = context;
            _settingService = settingService;
        }
        
        public IActionResult Index(ViewModel model)
        {
            var favIcon = _settingService.GetSetting().FavIconUrl;
            ViewBag.FavIcon = favIcon;

            model.PageComponents = _context.PageComponent.Where(x => x.IsDeleted == false).ToList();
            model.News = _context.News.Where(x => x.IsDeleted == false).ToList();
            model.SocialMedias = _context.SocialMedia.Where(x => x.IsDeleted == false).ToList();
            model.Addresses = _context.Adress.Where(x => x.IsDeleted == false).ToList();
            model.Sliders = _context.Slider.Where(x => x.IsDeleted == false).ToList();
            model.StaticPages = _context.StaticPage.Where(x => x.IsDeleted == false).ToList();
           

            return View(model);
        }
    }
}
