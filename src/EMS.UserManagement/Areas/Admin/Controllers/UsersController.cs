using System.Collections.Generic;
using System.Linq;
using EMS.CachingDbContext;
using EMS.CachingDbContext.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EMS.UserManagement.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class UsersController : Controller
    {
        private readonly CachingContext _context;

        public UsersController(CachingContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            return View(_context.Users.ToList());
//            var roots = _context.Users.Include(x => x.Manages).Where(x => x.Manager == null);
            
            var roots = new List<User>
            {
                new User
                {
                    FullName = "Gosho Pesho",
                    Manages = new List<User>
                    {
                        new User
                        {
                            FullName = "Pesho Sasho"
                        },
                        new User {FullName = "Sasho Tisho"}
                    }
                },
                new User
                {
                    FullName = "Gosho Pesho",
                    Manages = new List<User>
                    {
                        new User
                        {
                            FullName = "Pesho Sasho"
                        },
                        new User {FullName = "Sasho Tisho"}
                    }
                }
            };

            return View(roots);
        }
    }
}