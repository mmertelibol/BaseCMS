using System.Threading.Tasks;
using Common;
using Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Models.Shared
{
    public class LocationSelectViewComponent : ViewComponent
    {
        private readonly IUserService _userService;
      

        public LocationSelectViewComponent(IUserService userService)
        {
            _userService = userService;
          
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            

            return View("");
        }
    }
}