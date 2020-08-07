using Microsoft.AspNetCore.Mvc;

namespace NbSites.Web.Areas.Portal.Controllers
{
    public class HomeController : ControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
