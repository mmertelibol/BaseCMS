using System;
using System.Linq;
using System.ComponentModel;
using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Localization;

namespace Common
{
    public static class HttpHelper
    {
        public static string ConnStr;
        private static IConfiguration _config;
        private static IHttpContextAccessor _accessor;

        public static void Configure(IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            _config = config;
            _accessor = httpContextAccessor;
        }

        public static string DomainName
        {
            get
            {

                //if (_config["AppConfig:ActiveConnection"] == "test")
                //{
                //    var overrightport = _accessor.HttpContext.Request.Host.Port == 30061 ? 30021 :  + _accessor.HttpContext.Request.Host.Port;

                //    return _accessor.HttpContext.Request.Scheme + "://" + (_accessor.HttpContext.Request.Host.Host.Contains("localhost") == true ? "31.145.176.136" : _accessor.HttpContext.Request.Host.Host) +  (overrightport != null ? ":"+ overrightport : string.Empty);

                //}
                //else
                //{
                //    return _accessor.HttpContext.Request.Scheme + "://" + _accessor.HttpContext.Request.Host;
                //}      
                return _config["AppConfig:WebsiteUrl"];
            }
        }

        public static HttpContext HttpContext => _accessor?.HttpContext;

        public static IConfiguration Configuration => _config;

        public static T GetService<T>()
        {
            return _accessor != null &&
                _accessor.HttpContext != null &&
                _accessor.HttpContext.RequestServices != null
                    ? _accessor.HttpContext.RequestServices.GetService<T>()
                    : default(T);
        }

        public static string GetConfig(string key)
        {
            return GetConfig<string>(key);
        }

        public static T GetConfig<T>(string key)
        {
            var appSetting = _config[key.Trim()];

            if (string.IsNullOrWhiteSpace(appSetting))
                throw new Exception(key);

            var converter = TypeDescriptor.GetConverter(typeof(T));
            return (T)(converter.ConvertFromInvariantString(appSetting));
        }

        public static int? GetActiveUserId()
        {
            int? id = null;
            try
            {

                if (HttpContext == null || HttpContext.User == null || !HttpContext.User.Identity.IsAuthenticated)
                    return id;
                else
                {
                    var claim = ((ClaimsIdentity)HttpContext.User.Identity).Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                    if (claim != null)
                        id = Convert.ToInt32(claim.Value);
                }
            }
            catch (Exception)
            {
                id = null;
            }


            return id;
        }

        public static string GetActiveUserRole()
        {

            try
            {

                if (HttpContext == null || HttpContext.User == null || !HttpContext.User.Identity.IsAuthenticated)
                    return null;
                else
                {
                    var claim = ((ClaimsIdentity)HttpContext.User.Identity).Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);
                    if (claim != null)
                        return claim.Value;
                    else return null;
                }
            }
            catch (Exception)
            {
                return null;
            } 
        }

        public static bool HasEmployeeRole()
        {
            if (HttpContext == null || HttpContext.User == null || !HttpContext.User.Identity.IsAuthenticated)
                return false;
            return ((ClaimsIdentity)HttpContext.User.Identity).Claims.Any(x => x.Type == ClaimTypes.Role && x.Value == "employee");
        }

        public static int GetActiveHostUserId()
        {
            int id = 0;
            try
            {

                if (HttpContext?.User == null || !HttpContext.User.Identity.IsAuthenticated)
                    return id;
                else
                {
                    var claim = ((ClaimsIdentity)HttpContext.User.Identity).Claims.FirstOrDefault(x => x.Type == "apihostid");
                    if (claim != null)
                        id = Convert.ToInt32(claim.Value);
                }
            }
            catch (Exception)
            {
                id = 0;
            }


            return id;
        }

        public static int GetActiveUserIdForIpad()
        {
            int id = 0;
            try
            {

                if (HttpContext?.User == null || !HttpContext.User.Identity.IsAuthenticated)
                    return id;
                else
                {
                    var claim = ((ClaimsIdentity)HttpContext.User.Identity).Claims.FirstOrDefault(x => x.Type == "apiuserid");
                    if (claim != null)
                        id = Convert.ToInt32(claim.Value);
                }
            }
            catch (Exception)
            {
                id = 0;
            }


            return id;
        }

        public static int GetActiveCargoApiUserId()
        {
            int id = 0;
            try
            {

                if (HttpContext?.User == null || !HttpContext.User.Identity.IsAuthenticated)
                    return id;
                else
                {
                    var claim = ((ClaimsIdentity)HttpContext.User.Identity).Claims.FirstOrDefault(x => x.Type == "cargoAccessUser");
                    if (claim != null)
                        id = Convert.ToInt32(claim.Value);
                }
            }
            catch (Exception)
            {
                id = 0;
            }


            return id;
        }

        public static string GetClientIp()
        {
            var ip = HttpContext.Connection.RemoteIpAddress.ToString();
            return ip;
        }

        public static CultureInfo CurrentCulture =>
            HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture;
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class ParentActionsAttribute : Attribute
    {
        public readonly string ParentActions;

        public ParentActionsAttribute(string parMainActions)
        {
            ParentActions = parMainActions;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class SearchOverrideAttribute : Attribute
    {
        public readonly string SearchOverride;

        public SearchOverrideAttribute(string parSearchOverride)
        {
            SearchOverride = parSearchOverride;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreExcelAttribute : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class OpenInExcelAttribute : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreCacheAttribute : Attribute
    {

    }
}