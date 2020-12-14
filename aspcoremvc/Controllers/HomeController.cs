using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Data;
using Web.Models;
using Domain.User;
using Common.Enums;
using Common.Helpers;
using Business.Helper;
using Common.Resources;
using Business.Services;
using Common.Dto.DataTablesGrid;
using Business.Services.Interfaces;
using Common;
using Common.Dto;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Serilog;

namespace Web.Controllers
{

    public class HomeController : BaseController
    {
        private readonly LocService _localizer;

        private readonly CacheBase _cache;
        private readonly AppDbContext _context;
       

        public HomeController(CacheBase parCache, LocService parLocalizer, AppDbContext appDbContext)
        {
            _cache = parCache;        
            _localizer = parLocalizer;
            _context = appDbContext;         
        }

        [ResponseCache(Duration = 60, VaryByQueryKeys = new string[] { "lang" })]
        [AllowAnonymous]
        public JavaScriptResult GetAllResx(string lang)
        {
            var x = _localizer.GetAll(new System.Globalization.CultureInfo(lang));
            var resourceObjectName = "allresx";
            var sb = new StringBuilder();

            sb.AppendFormat("var {0}=[];", resourceObjectName);

            foreach (var item in x)
                sb.AppendFormat("{0}.push({{key:'{1}',value:'{2}'}});", resourceObjectName,
                    System.Web.HttpUtility.JavaScriptStringEncode(item.Name),
                    System.Web.HttpUtility.JavaScriptStringEncode(item.Value));

            return new JavaScriptResult(sb.ToString());
        }



        #region Cache
        [AllowAnonymous]
        public IActionResult Cache()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> GetCaches(DataTableAjaxPostModel model)
        {
            var caches = _cache.GetAllKeys();
            var query = caches.AsQueryable();
            var res = await query.ApplyGridBase(model);

            return Json(res);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult RemoveCache(string id)
        {
            _cache.RemoveCache(id);
            return Json(new ServiceResult { Code = ResultCode.Success, Message = "islembasarili" });
        }

        #endregion

        public async Task<IActionResult> Index()
        {
            //var logintypes = GlobalHelper.GetGroupConstants<LoginTypes>();
            //var ldaptype = GlobalHelper.GetConstantId(LoginTypes.Ldap);


            //var notification = await _notificationService.GetNotification(2);
            //notification.Message = "İkinci Bildirim";

            //await _notificationService.UpdateNotification(notification);




            //var mailTemplate = System.IO.File.ReadAllText("./MailTemplates/CheckIn.html");
            //var pattern = @"~#~\$getexternalparamvalue\('(.*?)'\)~#~";
            //var mailBody = Regex.Replace(mailTemplate, pattern, m =>
            //{
            //    var key = m.Groups[1].Value;
            //    switch (key)
            //    {
            //        case "DateNow":
            //            return DateTime.Now.Year.ToString();
            //        case "Id":
            //            return "1";
            //        case "ApplicationUrl":
            //            return HttpHelper.DomainName;
            //        default:
            //            return string.Empty;
            //    }
            //});

            //await  EmailHelper.SendMail("sergenekici@outlook.com", "", "Check-In ", mailBody);




            //var rabbit = HttpHelper.GetService<IModel>();
            //string message = "Hello World!";
            //var body = Encoding.UTF8.GetBytes(message);

            //rabbit.BasicPublish(exchange: "",
            //                     routingKey: "hellox",
            //                     basicProperties: null,
            //                     body: body);
            //var x = RestHelper.GetRequest("", "api/GetTransmissions", null, false);
            //cache.InsertCache(CacheBase.GetKeyString(CacheKeys.Genel.Araclar), 123);
            //var x = await testService.testquery();
            //var y = 0;
            //var z = 10 / y;

            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> SignalrTest()
        {
            //await _bus.Publish(new Message { Value = "1234" });
            return View();
        }

        [AllowAnonymous]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            return LocalRedirect(returnUrl);
        }

        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }

        [AllowAnonymous]
        public async Task<IActionResult> Template()
        {
            return View();
        }
    }
}