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
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ISettingService _settingService;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager, ISettingService settingService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _settingService = settingService;
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
            var email = await _userManager.FindByEmailAsync(user.Email);
                var result = await _signInManager.PasswordSignInAsync(email, user.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Homepage");
                }
                else
                {
                    ModelState.AddModelError("", "kullanıcı adı veya şifre hatalı");
                }

            return View(user);

        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
