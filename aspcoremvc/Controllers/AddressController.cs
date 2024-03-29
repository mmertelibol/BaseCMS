﻿using Business.Services.Panel.Interfaces;
using Common.Dto.PanelDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class AddressController : BaseController
    {
        private readonly IAddressService _addressService;
        private readonly ILogger<AddressController> _logger;
        private readonly ISettingService _settingService;
        public AddressController(IAddressService addressService, ILogger<AddressController> logger, ISettingService settingService)
        {
            _addressService = addressService;
            _logger = logger;
            _settingService = settingService;
        }
        public IActionResult Index()
        {

            var favIcon = _settingService.GetSetting().FavIconUrl;
            ViewBag.FavIcon = favIcon;
            var AddressList = _addressService.GetAllAddress();
            return View(AddressList);
        }

        [HttpPost]
        public JsonResult AddAddress(AddressDto addressDto)
        {
            var added = _addressService.AddAddress(addressDto);
            return Json(added);
        }

        [HttpDelete]
        public JsonResult DeleteAddress(int id)
        {
            var deleted = _addressService.DeleteAddress(id);
            return Json(deleted);
        }
        [HttpPost]
        public JsonResult UpdateAddress(AddressDto addressDto)
        {
            var updated = _addressService.UpdateAddress(addressDto);
            return Json(updated);
        }
        public JsonResult GetAddressById(int id)
        {
            var address = _addressService.GetAddressById(id);
            return Json(address);
        }
    }
}
