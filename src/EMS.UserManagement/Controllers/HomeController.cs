using System.Collections.Generic;
using System.Text;
using EMS.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EMS.UserManagement.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => RedirectToAction(nameof(Index), "Home", new {area = "Admin"});
    }
}