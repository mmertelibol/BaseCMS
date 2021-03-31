using Business.Services.Interfaces;
using Business.Services.Panel.Interfaces;
using Common.Dto;
using Common.Dto.PanelDtos;
using Domain.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class UserController : BaseController
    {

        private readonly SignInManager<User> _signInManager;
        
        private readonly ISettingService _settingService;
        private readonly IUserService  _userService;
        public UserController( SignInManager<User> signInManager, IUserService userService, ISettingService settingService)
        {
           
            _signInManager = signInManager;
            _userService = userService;
            _settingService = settingService;
        }

        public IActionResult Index()
        {
            var favIcon = _settingService.GetSetting().FavIconUrl;
            ViewBag.FavIcon = favIcon;

            var userList = _userService.GetAllUser();
            return View(userList);
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
            
            return RedirectToAction("Login", "User");
        }


        public async Task<JsonResult> AssignRole(int id)
        {
           
            var roleList = await _userService.AssignRole(id);
            return Json(JsonConvert.SerializeObject(roleList));
        }


        [HttpPost]
        public async Task<JsonResult> AssignRole(List<UserDto> assignRoles, int id)
        {
            var roleList = await _userService.AssignRolePost(assignRoles, id);

            return Json(roleList);
        }






    }
}
