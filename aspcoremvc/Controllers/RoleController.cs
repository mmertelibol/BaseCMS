using Business.Services.Interfaces;
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

        public RoleController( IAccountService accountService, ILogger<RoleController> logger)
        {
            _logger = logger;
           
            _accountService = accountService;
        }
        public IActionResult Index()
        {
           var roleList= _accountService.GetAllRoles();
            return View(roleList);
        }

        [HttpPost]
        public JsonResult AddRole(RoleDto role)
        {
            
            return Json(_accountService.AddRole(role));
        }
    }
}
