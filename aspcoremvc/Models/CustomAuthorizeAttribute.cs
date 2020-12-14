using System;
using System.Linq;
using Common;
using Business.Helper;
using Common.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Web.Models
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public CustomAuthorizeAttribute()
        {
            //_someFilterParameter = someFilterParameter;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            var actionDescriptor = (context.ActionDescriptor as ControllerActionDescriptor);
            var hasAnonymous = actionDescriptor.MethodInfo.GetCustomAttributes(inherit: true)
                .Any(a => a.GetType().Equals(typeof(AllowAnonymousAttribute)));

            if (hasAnonymous)
                return;

            if (!user.Identity.IsAuthenticated)
            {
                //context.Result = new UnauthorizedResult();
                return;
            }

            var actions = new string[] { actionDescriptor.ActionName };
            var parentAction = actionDescriptor.MethodInfo.GetCustomAttributes(inherit: true)
                .FirstOrDefault(a => a.GetType() == typeof(ParentActionsAttribute));

            if (parentAction != null)
                actions = (parentAction as ParentActionsAttribute).ParentActions.Split(',');

            var userPages = GlobalHelper.GetUserPages();
            var thisPageClaim = userPages
                .FirstOrDefault(p => p.Controller == actionDescriptor.ControllerName && actions.Contains(p.Action));
            
            //TODO:YetkinizYok kısmı düzeltilecek mi?
            if (thisPageClaim == null)
            {
                //context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
                context.Result = new RedirectResult("/Account/YetkinizYok");
                //context.Result = new UnauthorizedResult();
                return;
            }

            var localizer = HttpHelper.GetService<LocService>();
            context.RouteData.Values.Add("Title", localizer.Get(thisPageClaim.TitleCode));
        }
    }
}