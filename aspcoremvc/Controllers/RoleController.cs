using Business.Services.Interfaces;
using Business.Services.Panel.Interfaces;
using Common.Dto;
using Domain.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class RoleController : BaseController
    {
      
        private readonly IAccountService _accountService;
        private readonly ILogger<RoleController> _logger;
        private readonly ISettingService _settingService;

        public RoleController( IAccountService accountService, ILogger<RoleController> logger, ISettingService settingService)
        {
            _logger = logger;
            _settingService = settingService;
            _accountService = accountService;
        }
        public IActionResult Index()
        {
            var favIcon = _settingService.GetSetting().FavIconUrl;
            ViewBag.FavIcon = favIcon;

            var roleList= _accountService.GetAllRoles();
            return View(roleList);
        }

        [HttpPost]
        public JsonResult AddRole(RoleDto role)
        {
            
            return Json(_accountService.AddRole(role));
        }

        [HttpPost]
        public JsonResult UpdateRole(RoleDto role)
        {

            return Json(_accountService.UpdateRole(role));
        }
        public JsonResult GetRoleById(int roleId)
        {

            return Json(_accountService.GetRoleById(roleId));
        }

        [HttpDelete]
        public JsonResult DeleteRole(int roleId)
        {

            return Json(_accountService.DeleteRole(roleId));
        }




    }
}
