using Microsoft.AspNetCore.Mvc;

namespace EMS.UserManagement.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}