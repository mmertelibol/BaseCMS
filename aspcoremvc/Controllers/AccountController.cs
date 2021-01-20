using Business.Services.Interfaces;
using Business.Services.Panel.Interfaces;
using Common.Dto;
using Domain.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class AccountController : BaseController
    {

        private readonly SignInManager<User> _signInManager;
        
        private readonly ISettingService _settingService;
        private readonly IUserService  _userService;
        public AccountController( SignInManager<User> signInManager, IUserService userService, ISettingService settingService)
        {
           
            _signInManager = signInManager;
            _userService = userService;
            _settingService = settingService;
        }

        public IActionResult Index()
        {
            var favIcon = _settingService.GetSetting().FavIconUrl;
            ViewBag.FavIcon = favIcon;
            return View();
        }
        public IActionResult Login()
        {
            var favIcon = _settingService.GetSetting().FavIconUrl;
            ViewBag.FavIcon = favIcon;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserDto user)
        {
            if (ModelState.IsValid)
            {
                var login= await _userService.Login(user);
                if (login==true)
                {
                    return RedirectToAction("Index", "HomePage");
                }
               

            }
            ModelState.AddModelError("", "Kullanıcı Adı veya Şifre Hatalı");
            return View(user);

        }

        public async Task<IActionResult> LogOut()
        {
           await _userService.SignOut();
            
            return RedirectToAction("Login", "Account");
        }
    }
}
