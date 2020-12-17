using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class NewsCategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult InsertNewsCategory()
        {
            return Json("true");
        }

        public JsonResult DeleteNewsCategory()
        {
            return Json("true");
        }

        public JsonResult UpdateNewsCategory()
        {
            return Json("true");
        }

        public JsonResult GetCategoryById()
        {
            return Json("true");
        }
    }
}
