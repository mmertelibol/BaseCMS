using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Business.Helper;

namespace Web.Models.Shared
{
    public class RenderMenuViewComponent : ViewComponent
    {
        public RenderMenuViewComponent()
        {

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userpages = GlobalHelper.GetUserPages()
                .Where(p => !p.HideInMenu)
                .ToList();

            return View(userpages);
        }
    }
}