using System;
using System.Linq;
using System.Security.Claims;
using Data;
using Common;
using Common.Dto;
using Common.Helpers;
using Common.Dto.DataTablesGrid;
using Business.Services.Interfaces;
using Domain.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Web.Controllers
{
    public class BaseController : Controller
    {
        private AppDbContext _dbContext;
        private bool _ignoreLog;
        private int _actionLogId;
        private bool _hasTransaction;


        //public override void OnActionExecuting(ActionExecutingContext context)
        //{
        //    var descriptor = context.ActionDescriptor as ControllerActionDescriptor;

        //    if (HttpHelper.GetConfig<bool>("AppConfig:LogActions"))
        //    {
        //        _ignoreLog = descriptor.MethodInfo.GetCustomAttributes(inherit: true)
        //            .Any(a => a.GetType().Equals(typeof(IgnoreLogAttribute)));

        //        if (!_ignoreLog)
        //        {
        //            string prms = null;

        //            if (context.HttpContext.Request.Method == "POST"
        //                && context.HttpContext.Request.ContentType != null)
        //            {
        //                var form = context.HttpContext.Request.Form;
        //                prms = form.Keys.ToDictionary(k => k, k => form[k]).JsonEncode();

        //            }
        //            else if (context.HttpContext.Request.Method == "GET")
        //                prms = context.HttpContext.Request.Query.JsonEncode();

        //            var actionLog = new ActionLogDto
        //            {
        //                Controller = descriptor.ControllerName,
        //                Action = descriptor.ActionName,
        //                RequestStart = DateTime.Now,
        //                UserId = HttpHelper.GetActiveUserId(),
        //                ClientIp = HttpHelper.GetClientIp(),
        //                Parameters = prms,
        //                Referer = context.HttpContext.Request.Headers["Referer"].Count == 0
        //                    ? context.HttpContext.Request.GetDisplayUrl()
        //                    : context.HttpContext.Request.Headers["Referer"].ToString(),
        //            };

        //            _actionLogId = HttpHelper.GetService<IAccountService>().AddActionLog(actionLog);
        //        }
        //    }


        //    _hasTransaction = descriptor.MethodInfo.GetCustomAttributes(inherit: true)
        //    .Any(a => a.GetType().Equals(typeof(HasTransactionAttribute)));

        //    if (_hasTransaction)
        //    {
        //        _dbContext = HttpHelper.GetService<AppDbContext>();
        //        _dbContext.Database.BeginTransaction();

        //    }
        //}

        //public override void OnActionExecuted(ActionExecutedContext context)
        //{
        //    if (_hasTransaction && _dbContext.Database.CurrentTransaction != null)
        //    {
        //        if (context.Exception == null)
        //            _dbContext.Database.CommitTransaction();
        //        else
        //            _dbContext.Database.RollbackTransaction();
        //    }

        //    if (HttpHelper.GetConfig<bool>("AppConfig:LogActions"))
        //        HttpHelper.GetService<IAccountService>().UpdateActionLog(_actionLogId, context.Exception != null);
        //}

        public void AddMultipleSearch<T>(DataTableAjaxResponse res)
            where T : GridRowBaseKeyed
        {
            var searchitem = (T)Activator.CreateInstance(typeof(T));
            searchitem.IsMultipleSearch = true;
            res.data.Insert(0, searchitem);
        }

        public void SetCookie(string key, string value, CookieOptions options)
        {
            HttpContext.Response.Cookies.Append(key, value, options);
        }

        public string GetCookie(string key)
        {
            return HttpContext.Request.Cookies[key];
        }
    }

    public class JavaScriptResult : ContentResult
    {
        public JavaScriptResult(string script)
        {
            Content = script;
            ContentType = "application/javascript";
        }
    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class HasTransactionAttribute : ActionFilterAttribute
    {

    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class IgnoreLogAttribute : ActionFilterAttribute
    {

    }
}