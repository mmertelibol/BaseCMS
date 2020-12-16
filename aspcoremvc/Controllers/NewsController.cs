using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class NewsController : Controller
    {

        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public JsonResult InsertNews()
        {

            return Json("true");
        }

        [HttpDelete]
        public JsonResult DeleteNews()
        {

            return Json("true");

        }

        [HttpPut]
        public JsonResult UpdateNews()
        {

            return Json("true");

        }

        [HttpGet]
        public JsonResult GetNewsById(int id)
        {

            return Json("true");

        }




    }



}
