using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.CachingDbContext;
using EMS.CachingDbContext.Models;
using EMS.Contracts.Workers;
using EMS.UserManagement.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            return View(_context.Users.Include(x => x.Manager).ToList());
        }

        public IActionResult Create()
        {
            ViewBag.Users = _context.Users.Select(x => new SelectListItem(x.FullName, x.Id.ToString()));
            return View();
        }

        public async Task<IActionResult> Add([FromForm] AddUserModel model)
        {
            if (!ModelState.IsValid) return BadRequest();
            await _context.Users.AddAsync(new User
            {
                EmailAddress = model.EmailAddress,
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                UserName = model.UserName,
                Manager = await _context.Users.SingleOrDefaultAsync(x => x.Id.ToString() == model.ManagerId)
            });

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(Guid id)
        {
            _context.Users.Remove(await _context.Users.SingleAsync(x => x.Id == id));
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(Guid id) =>
            View(await _context.Users.Include(x => x.Manager).Include(x => x.Manages).SingleAsync(x => x.Id == id));
    }
}