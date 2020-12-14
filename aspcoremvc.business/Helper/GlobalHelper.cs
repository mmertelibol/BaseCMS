using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;
using Common;
using Common.Dto;
using Common.Helpers;
using Business.Services.Interfaces;
using Common.Resources;
using System;

namespace Business.Helper
{
    public static class GlobalHelper
    {
        public static List<PageDto> GetUserPages()
        {
            var cacheService = HttpHelper.GetService<CacheBase>();
            var accountService = HttpHelper.GetService<IAccountService>();
            var allMenu = cacheService.CacheFetch(CacheKeys.Genel.AllMenu, () => accountService.GetAllMenu());

            var pageClaims = ((ClaimsIdentity)HttpHelper.HttpContext.User.Identity).Claims
                .Where(p => p.Type == "pid")
                .Select(p => int.Parse(p.Value))
                .ToList();

            var userPages = allMenu
                .Where(p => pageClaims.Contains(p.Id))
                .ToList();

            return userPages;
        }

    
    }
}