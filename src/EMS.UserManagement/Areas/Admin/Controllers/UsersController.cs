using System;
using System.Linq;
using System.Threading.Tasks;
using EMS.CachingDbContext;
using EMS.CachingDbContext.Models;
using EMS.Contracts.DataAccess;
using EMS.UserManagement.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EMS.UserManagement.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class UsersController : Controller
    {
        private readonly CachingContext _context;
        private readonly IDataSource _dataSource;

        public UsersController(CachingContext context, IDataSource dataSource)
        {
            _context = context;
            _dataSource = dataSource;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _dataSource.Entities<User>().Include(x => x.Manager).ToListAsync());
//            return View(_context.Users.Include(x => x.Manager).ToList());
        }

        public IActionResult Create()
        {
            ViewBag.Users = _dataSource.Entities<User>().Select(x => new SelectListItem(x.FullName, x.Id.ToString()));
//            ViewBag.Users = _context.Users.Select(x => new SelectListItem(x.FullName, x.Id.ToString()));
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
//                Manager = await _context.Users.SingleOrDefaultAsync(x => x.Id.ToString() == model.ManagerId)
                Manager = await _dataSource.Entities<User>()
                    .SingleOrDefaultAsync(x => x.Id.ToString() == model.ManagerId)
            });

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(Guid id)
        {
//            _context.Users.Remove(await _context.Users.SingleAsync(x => x.Id == id));
            _context.Users.Remove(await _dataSource.Entities<User>().SingleAsync(x => x.Id == id));
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(Guid id) =>
            View(await _dataSource.Entities<User>().Include(x => x.Manager).Include(x => x.Manages)
                .SingleAsync(x => x.Id == id));

//            View(await _context.Users.Include(x => x.Manager).Include(x => x.Manages).SingleAsync(x => x.Id == id));
    }
}