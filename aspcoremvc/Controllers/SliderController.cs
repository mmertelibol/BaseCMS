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
    
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly ILogger<SliderController> _logger;
        public SliderController(ISliderService sliderService, ILogger<SliderController> logger)
        {
            _logger = logger;
            _sliderService = sliderService;

        }
        public IActionResult Index()
        {
            var sliderList = _sliderService.GetAllSliders();
            return View(sliderList);
        }

        [HttpPost]
        public JsonResult AddSlider(SliderDto sliderDto)
        {
            var added = _sliderService.AddSlider(sliderDto);
            return Json(added);
        }

        [HttpDelete]
        public JsonResult DeleteSlider(int id)
        {
            var deleted = _sliderService.DeleteSlider(id);
            return Json(deleted);
        }

        [HttpPut]
        public JsonResult UpdateSlider(SliderDto sliderDto)
        {
            var updated = _sliderService.UpdateSlider(sliderDto);
            return Json(updated);
        }

        public JsonResult GetSliderById(int id)
        {
            var slider = _sliderService.GetSliderById(id);
            return Json(slider);
        }
    }
}
