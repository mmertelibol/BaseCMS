using System.Threading.Tasks;
using Common;
using Common.Dto;
using Domain.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Helper
{
    public class UserHelper
    {
        public static async Task<User> GetUser()
        {
            var userManager = HttpHelper.HttpContext.RequestServices.GetRequiredService<UserManager<User>>();
            return await userManager.GetUserAsync(HttpHelper.HttpContext.User);
        }

        //public static LocationDto GetUserLocation()
        //{
           
        //}
    }
}